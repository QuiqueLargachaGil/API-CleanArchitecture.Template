import re
import json
import argparse
from pathlib import Path

TEMPLATE_NAME = "API.CleanArchitectureTemplate"
SOLUTION_FILE = "API.CleanArchitecture.Template.slnx"

EXCLUDED_DIRS = {".git", "bin", "obj", ".vs", ".idea", ".vscode"}


def is_valid_project_name(name: str) -> bool:
    return re.match(r"^[A-Za-z][A-Za-z0-9.]*$", name) is not None


def prompt_user():
    project_name = input("Enter project name: ").strip()

    if not is_valid_project_name(project_name):
        raise ValueError("Invalid project name. Use letters, numbers and dots only.")

    author = input("Author name: ").strip()
    company = input("Company name: ").strip()

    return project_name, author, company


def should_skip(path: Path) -> bool:
    return any(part in EXCLUDED_DIRS for part in path.parts)


def replace_in_file(file_path, old, new, dry_run):
    try:
        content = file_path.read_text(encoding="utf-8")
        new_content = content.replace(old, new)

        if content != new_content:
            print(f"[UPDATE FILE] {file_path}")

            if not dry_run:
                file_path.write_text(new_content, encoding="utf-8")

            return 1

    except Exception as e:
        print(f"[ERROR] Failed processing {file_path}: {e}")

    return 0


def replace_in_all_files(root, old, new, dry_run):
    count = 0

    for path in root.rglob("*"):
        if should_skip(path):
            continue

        if path.is_file() and path.suffix in [".cs", ".csproj", ".slnx", ".json", ".http", ".md"]:
            count += replace_in_file(path, old, new, dry_run)

    return count


def rename_files(root, old, new, dry_run):
    count = 0

    # Sort paths so files are renamed before directories
    paths = sorted(root.rglob("*"), key=lambda p: len(str(p)), reverse=True)

    for path in paths:
        if should_skip(path):
            continue

        if old in path.name:
            new_name = path.name.replace(old, new)
            new_path = path.with_name(new_name)

            if path.is_file():
                print(f"[RENAME FILE] {path} → {new_path}")
            elif path.is_dir():
                print(f"[RENAME DIR ] {path} → {new_path}")

            if not dry_run:
                try:
                    path.rename(new_path)
                except Exception as e:
                    print(f"[ERROR] Failed renaming {path}: {e}")
                    continue

            count += 1

    return count


def rename_solution(root, project_name, dry_run):
    solution_path = root / SOLUTION_FILE

    if solution_path.exists():
        new_name = f"{project_name}.API.slnx"
        new_path = root / new_name

        print(f"[RENAME FILE] {solution_path} → {new_path}")

        if not dry_run:
            solution_path.rename(new_path)

        return 1

    return 0


def create_readme(root, project_name, dry_run):
    readme_path = root / "README.md"

    content = f"# {project_name}\n"

    print("[UPDATE FILE] README.md")

    if not dry_run:
        readme_path.write_text(content, encoding="utf-8")

    return 1


def update_stylecop(root, author, company, dry_run):
    stylecop_path = root / "stylecop.json"

    if not stylecop_path.exists():
        print("[INFO] No stylecop.json found")
        return 0

    try:
        data = json.loads(stylecop_path.read_text(encoding="utf-8"))

        rules = data.get("settings", {}).get("documentationRules", {})

        # Update company
        rules["companyName"] = company

        # Update variables
        variables = rules.get("variables", {})
        variables["author"] = author

        license_name = variables.get("licenseName", "MIT")
        license_file = variables.get("licenseFile", "LICENSE")

        # Update copyright text (KEEP placeholders)
        rules["copyrightText"] = (
            "Copyright (c) {author}.\n"
            "All rights reserved.\n\n"
            "Licensed under the {licenseName} license. "
            "See {licenseFile} file in the project root for full license information."
        )

        print("[UPDATE FILE] stylecop.json")

        if not dry_run:
            stylecop_path.write_text(json.dumps(data, indent=2), encoding="utf-8")

        return 1

    except Exception as e:
        print(f"[ERROR] Failed updating stylecop.json: {e}")
        return 0


def build_copyright_lines(author, license_name, license_file):
    """
    Builds the copyright block exactly as StyleCop expects,
    converting it into C# comment lines.
    """

    lines = [
        f"// Copyright (c) {author}.",
        "// All rights reserved.",
        "//",
        f"// Licensed under the {license_name} license. See {license_file} file in the project root for full license information."
    ]

    return "\n".join(lines)


def update_file_headers(root, company, author, license_name, license_file, dry_run):
    count = 0

    new_copyright = build_copyright_lines(author, license_name, license_file)

    pattern = re.compile(
        r"(// <copyright file=\".*?\" company=\")(?P<company>.*?)(\">)(?P<body>.*?)(// </copyright>)",
        re.DOTALL
    )

    for path in root.rglob("*.cs"):
        if should_skip(path):
            continue

        try:
            content = path.read_text(encoding="utf-8")

            match = pattern.search(content)
            if not match:
                continue

            new_block = (
                f'{match.group(1)}{company}{match.group(3)}\n'
                f'{new_copyright}\n'
                f'// </copyright>'
            )

            new_content = pattern.sub(new_block, content)

            if content != new_content:
                print(f"[UPDATE HEADER] {path}")

                if not dry_run:
                    path.write_text(new_content, encoding="utf-8")

                count += 1

        except Exception as e:
            print(f"[ERROR] Header update failed {path}: {e}")

    return count


def main():
    parser = argparse.ArgumentParser(description="Bootstrap Clean Architecture Template")
    parser.add_argument("--dry-run", action="store_true", help="Preview changes without applying them")

    args = parser.parse_args()
    dry_run = args.dry_run

    root = Path(__file__).resolve().parent.parent

    print(f"\nProject root: {root}")
    print("Clean Architecture Template Bootstrap")
    print("--------------------------------------\n")

    project_name, author, company = prompt_user()

    new_base = project_name

    print("\n--- Replacing content ---")
    updates = replace_in_all_files(root, TEMPLATE_NAME, new_base, dry_run)

    print("\n--- Renaming files and directories ---")
    renames = rename_files(root, TEMPLATE_NAME, new_base, dry_run)

    print("\n--- Renaming solution ---")
    sol = rename_solution(root, project_name, dry_run)

    print("\n--- Updating StyleCop ---")
    stylecop = update_stylecop(root, author, company, dry_run)

    print("\n--- Updating file headers ---")
    license_name = "MIT"
    license_file = "LICENSE"
    headers = update_file_headers(root, company, author, license_name, license_file, dry_run)

    print("\n--- Generating README ---")
    readme = create_readme(root, project_name, dry_run)

    print("\n--------------------------------------")
    print("Summary:")
    print(f"Updated files: {updates}")
    print(f"Renamed items: {renames}")
    print(f"Solution renamed: {sol}")
    print(f"StyleCop updated: {stylecop}")
    print(f"Headers updated: {headers}")
    print(f"README updated: {readme}")

    if dry_run:
        print("\n[DRY-RUN] No changes were applied.")
    else:
        print("\nProject successfully initialized.")


if __name__ == "__main__":
    main()