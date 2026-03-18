import os
import re
from pathlib import Path
import json

TEMPLATE_NAME = "API.CleanArchitectureTemplate"
SOLUTION_FILE = "API.CleanArchitecture.Template.slnx"


def prompt_user():
    project_name = input("Enter project name: ").strip()

    author = input("Author name: ").strip()
    company = input("Company name: ").strip()

    return project_name, author, company


def replace_in_file(file_path, old, new):
    try:
        content = file_path.read_text(encoding="utf-8")
        new_content = content.replace(old, new)

        if content != new_content:
            file_path.write_text(new_content, encoding="utf-8")
            print(f"Updated: {file_path}")

    except Exception as e:
        print(f"Failed processing {file_path}: {e}")


def replace_in_all_files(root, old, new):
    for path in root.rglob("*"):
        if path.is_file():
            if path.suffix in [".cs", ".csproj", ".slnx", ".json", ".http", ".md"]:
                replace_in_file(path, old, new)


def rename_files(root, old, new):
    paths = sorted(root.rglob("*"), key=lambda p: len(str(p)), reverse=True)

    for path in paths:
        if old in path.name:
            new_name = path.name.replace(old, new)
            new_path = path.with_name(new_name)

            try:
                path.rename(new_path)
                print(f"Renamed: {path} → {new_path}")
            except Exception as e:
                print(f"Failed renaming {path}: {e}")


def rename_solution(root, project_name):
    solution_path = root / SOLUTION_FILE

    if solution_path.exists():
        new_name = f"{project_name}.API.slnx"
        new_path = root / new_name
        solution_path.rename(new_path)
        print(f"Renamed solution: {solution_path} → {new_path}")


def create_readme(root, project_name):
    readme_path = root / "README.md"

    content = f"# {project_name}\n"

    readme_path.write_text(content, encoding="utf-8")

    print("README.md regenerated")


def update_stylecop(root, author, company):
    stylecop_path = root / "stylecop.json"

    if not stylecop_path.exists():
        print("No stylecop.json found")
        return

    try:
        data = json.loads(stylecop_path.read_text(encoding="utf-8"))

        if "settings" in data and "documentationRules" in data["settings"]:
            rules = data["settings"]["documentationRules"]

            rules["companyName"] = company
            rules["copyrightText"] = f"Copyright (c) {company}. All rights reserved."

        stylecop_path.write_text(json.dumps(data, indent=2), encoding="utf-8")

        print("stylecop.json updated")

    except Exception as e:
        print(f"Failed updating stylecop.json: {e}")


def main():
    root = Path(__file__).resolve().parent.parent

    print(f"Project root detected at: {root}")

    print("Clean Architecture Template Bootstrap")
    print("--------------------------------------")

    project_name, author, company = prompt_user()

    new_base = project_name

    print("\nUpdating file contents...")
    replace_in_all_files(root, TEMPLATE_NAME, new_base)

    print("\nRenaming files and folders...")
    rename_files(root, TEMPLATE_NAME, new_base)

    print("\nRenaming solution...")
    rename_solution(root, project_name)

    print("\nUpdating StyleCop...")
    update_stylecop(root, author, company)

    print("\nGenerating README...")
    create_readme(root, project_name)

    print("\nProject successfully initialized.")


if __name__ == "__main__":
    main()