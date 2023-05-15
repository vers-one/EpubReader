# Release checklist

## Checks

1. Release milestone doesn't have open issues.
2. Documentation has been updated with the changes from the new release.
3. README.md file is updated, if necessary.
4. All CI checks are passing for the last commit.

## Release procedure

1. Prepare the change log markdown file for the GitHub release description.
2. Run **Generate documentation** action.
3. Run **Update versions** action with version = `<major>.<minor>.<patch>` format (without `v` prefix). If this is a pre-release version which requires `-alpha` or `-beta` postfixes, the versions need to be updated via a manual code change.
4. Run **Publish to Nuget** action.
5. Run **Create release** action with version = `<major>.<minor>.<patch>` format (without `v` prefix). This action can also be used for pre-release versions which require `-alpha` or `-beta` postfixes.
6. Update the release description for the release draft generated at the previous step and publish the release.
7. Make sure that Nuget package published at the step 5 is visible on the Nuget website and the Nuget badge on the Readme page is pointing to the new release.
