# Template Bootstrap Script

This script bootstraps a new project from the Clean Architecture API template by automating the renaming and configuration process.

It replaces the base template naming (`API.CleanArchitectureTemplate`) with a user-defined project name and updates all related files accordingly.

## Purpose

The script is designed to:

- Rename solution and project files
- Update namespaces and internal references
- Regenerate the README file
- Update StyleCop configuration (company, author, license)
- Synchronize file headers across all `.cs` files
- Provide a safe preview mode (`--dry-run`)

It allows quickly transforming the template into a real project with consistent naming and structure.

## How It Works

The script performs the following steps:

1. Prompts the user for:
   - Project name
   - Author name
   - Company name

2. Replaces all occurrences of:

```text
API.CleanArchitectureTemplate → <ProjectName>
```

3. Renames:
- Project files (`.csproj`)
- Folders
- Solution file (`.slnx` → `<ProjectName>.API.slnx`)

4. Updates `stylecop.json`:
- `companyName`
- `variables.author`
- Keeps license placeholders (`{licenseName}`, `{licenseFile}`)
- Applies a production-ready copyright text

5. Updates all `.cs` file headers:
- Syncs company name
- Rebuilds the copyright block
- Ensures consistency with `stylecop.json`

6. Regenerates `README.md` with the project name

## Usage

Run the script from the repository root:

```bash
python utils/bootstrap.py
```
### Dry Run Mode

To preview changes without modifying files:

```bash
python utils/bootstrap.py --dry-run
```

This will:
- Show all planned updates and renames
- Not apply any changes to the filesystem

Recommended before first execution.

## Output Logs

The script prints structured logs:
- [UPDATE FILE] → file content updated
- [UPDATE HEADER] → file header updated
- [RENAME FILE] → file renamed
- [RENAME DIR] → directory renamed
- [ERROR] → operation failed

A summary is displayed at the end.

## Known Limitations / Failure Scenarios

### 1. Existing renamed directories

If the script is executed multiple times without cleaning the workspace:
- Previously created directories may already exist
- Renaming operations may fail or produce inconsistent results

Recommendation:
- Run the script only once on a fresh template
- Or clean the repository before re-running

### 2. Partial manual changes

If files or folders are manually modified before running the script:
- Some replacements may not be applied correctly
- Naming inconsistencies may appear

### 3. Unsupported file types

The script only processes:

`.cs, .csproj, .slnx, .json, .http, .md`

Other file types are ignored.

## StyleCop Behavior

The script ensures:
- `stylecop.json` remains the single source of truth
- File headers match the configured values
- License placeholders remain dynamic

Header format:
``` C#
// Copyright (c) <Author>.
// All rights reserved.
//
// Licensed under the <licenseName> license. See <licenseFile> file in the project root for full license information.
```

### Best Practices

- Always run with `--dry-run` before applying changes
- Use a clean repository state
- Commit template before running the script
- Review changes after execution

## Keep in Mind (Future Improvements)

The following enhancements were considered but intentionally postponed:
- Separation of file and directory renaming logic
- Automatic `.sln/.slnx` detection
- Placeholder-based templates (__PROJECT_NAME__)
- Non-interactive mode (CLI arguments)
- Support for multiple templates
- Regeneration of `.sln` instead of renaming
- CI/CD integration (dynamic workflow naming)

These can be implemented if the script evolves into a reusable tool.

## Conclusion

This script provides a fast and consistent way to initialize new projects from the template while enforcing naming, structure, and coding standards.