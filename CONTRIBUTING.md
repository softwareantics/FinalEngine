<h1 style="border:0;font-weight:bold" align="center">
  Contribution Guide
</h1>

Hello!! Thanks for your interest in contributing to _Final Engine_! Any help you can provide is greatly appreciated. We are always looking for ways to make the contribution process as simple and as straightforward as possible. In addition, we want to make sure that the required workflow and development processes are followed in order to maintain high software quality. If you have any ideas and/or comments on how to simplify the process for outside contributors, please don't hesitate to contact one of the project maintainers.

You can contribute to our projects with issues and PRs. Creating GitHub issues for problems you encounter and code contribution are other ways to help out and are greatly appreciated!

**Considerations:**

Only the **Final Engine** team will merge changes that improve the library significantly. We will not merge changes that have narrowly-defined benefits or are breaking in any way. All contributions must also follow all other guidelines outlined in this document.

<h2 style="border:0;font-weight:bold" align="center">DO's and DON'Ts</h2>

First off, don't be intimidated and the best thing you can do if you're having trouble following out contribution guidelines is to talk to us on our [Discord](https://discord.gg/UNdKXsdeQb) server.

**Please:**

- **DO** follow our [code of conduct](./CODE_OF_CONDUCT.md).
- **DO** talk to us on our [Discord](https://discord.gg/UNdKXsdeQb) server.
- **DO** keep discussions related to contributions centered around the issues at hand.
- **DO** use the comment sections in the issues and pull requests that are relevant to said issues and PRs. This not only keeps conversations contained in the issues and PRs, it sends notifications to the project maintainers, and shows the conversation to the community/public.
- **DO** leave a comment in the issue that you are interested in to let the project maintainers know that you would like to work on the issue.
- **DO** create PRs with the title matching **EXACTLY** to the associated issue's title. PRs with mismatching titles will not pass status checks and cannot be merged.
- **DO** use [pull request templates](./.github/pull_request_template.md) when creating PRs. Pull requests without a PR template will not be merged.
- **DO** use [Early Pull Requests](https://medium.com/practical-blend/pull-request-first-f6bb667a9b6).
- **DON'T** start working on a PR until you get confirmation from a project maintainer and the issue is assigned to you. We aim to ensure that you invest your time efficiently on issues that align with your permissions and contributions.
- **DON'T** make PRs that don't directly affect the end user, such as style changes. These are best done as part of a PR related to the area in question. Documentation is fine (and encouraged!), as is useful to the end user.
- **DON'T** commit code you didn't write without following its own license and without following our guidelines in the Contributor License Agreement. If you are unable to license code, don't commit it.
- **DON'T** surprise us with big PRs or big API changes without talking to us first - make a fork, prototype and communicate with us.
- **DON'T** make PRs for legal or administrative documents, such as the license, file headers, or code of conduct. If something is off, let us know and we will look into changing it.

<h2 style="border:0;font-weight:bold" align="center">Breaking Changes</h2>

Contributions must maintain API signature and behavioral compatibility. Contributions that include breaking changes **must** be discussed with project maintainers first. Breaking changes are subject to the team's approval. At risk of sounding like a broken record, talk to us about your idea first! Breaking changes include modifying the signature of a constructor, method, etc; if a breaking change is introduced as a result, it's likely we will choose to use the `[DeprecatedAttribute]` where possible.

<h2 style="border:0;font-weight:bold" align="center">Branching</h2>

We only have one main branch: `final`, why? **KISS** ([Keep it simple, stupid!](https://en.wikipedia.org/wiki/KISS_principle)).

Our projects follow a strict naming policy. The purpose is for clarity and consistency, and it helps bind the information between GIT and GitHub. Our project is split up into multiple areas (see the tags [here](https://github.com/softwareantics/FinalEngine/labels?q=area), we use this to our advantage when implementing features or fixing bugs. Below is a general guide on our naming policy:

- **Features:** `feature/[area]/issue-number` - an example would be `feature/rendering/423`.
- **Bugs:** `bug/[area]issue-number` - an example would be `bug/audio/451`.

Of course, we are all human and people make mistakes!! This is ok and no harm is done. If you do accidentally create a PR with a branch name that is incorrect, tag a maintainer in the PR about the mistake and recreate the PR. If for some reason you used the incorrect target branch, the PR does not have to be recreated; you should be able to edit the title and the target branch because you are the author of the PR.

<h2 style="border:0;font-weight:bold" align="center">Create a Branch to Contribute</h2>

For first-time contributors, there are a few steps that you will need to go through to start contributing.

#### **1. Let GIT know who you are**
To better track changes and who does what, it's a good practice to give GIT some information about yourself.
   ```cli
   git config --global user.name "John Doe"
   git config --global user.email "john.doe@example.com"
   ```

#### **2. Fork the repository**

[Fork](https://github.com/softwareantics/FinalEngine/fork) the _Final Engine_ repository.

> **Note**
> _For more information on how to fork a repository, go [here](https://docs.github.com/en/get-started/quickstart/fork-a-repo)._

#### **3. Clone the forked repository**

Clone the forked repository to your machine so you can add your changes.  Swap `johndoe` with your username.

```cli
git clone https://github.com/johndoe/FinalEngine.git
```

#### **4. Your First Commit**

Begin your work with an empty commit that will describe the feature or issue:

```cli
git commit --allow-empty -m "[WIP] Descriptive task name."`
```

#### **5. Create a Draft PR**

We encourage and use [Early Pull Requests](https://medium.com/practical-blend/pull-request-first-f6bb667a9b6). Please don't wait until you're finished with your work before creating a PR!

Create a **Draft** Pull Request with `[WIP` in the title, following the provided template. Do this **before** you start working. It is likely you won't be able to fill out much of the template until _after_ the work is completed. Furthermore, ensure that the title of the PR matches the title of the GitHub issue.

> **Important**
> _We will not merge PRs that do not attempt follow the template provided. It's important you take your time providing contextual information related to the PR including any relevant changes you've made or issues that may arise from the feature or bug fix implemented_.

#### **6. Make Changes**

Commit your changes in small, incremental steps so that the maintainers can review your changes easily. Most of the time we _squash_ merge PRs into the `master` branch but on occasion, depending on how large the feature is we may choose to do otherwise.

#### **7. Notify a Maintainer for Review**

Notify a maintainer when you're ready for your PR to be reviewed and merged. Please note that we have a strict PR review process, especially around _documentation_ and _styling_. If the PR does not meet the requirements you'll likely need to make some changes to merge your feature. You can find more information on our review process [here](./Review).

<h2 style="border:0;font-weight:bold" align="center">Recommended Extensions</h2>

The maintainers of _Final Engine_ use _Visual Studio_ and highly encourage installing the following extensions to make your life easier.

- [CodeMaid](https://www.codemaid.net)
- [Code Cleanup on Save](https://github.com/madskristensen/CodeCleanupOnSave)
- [Collapse Comments](https://github.com/mrlacey/CollapseComments)
- [Visual Studio Spell Checker](https://ewsoftware.github.io/VSSpellChecker)
- [Final Code Coverage](https://marketplace.visualstudio.com/items?itemName=FortuneNgwenya.FineCodeCoverage)
- [GhostDoc Community](https://www.submain.com/products/ghostdoc.aspx)

Whilst it is not a _requirement_ to install these extensions and use them in your workflow you'll likely find that installing them will make your development process move smoothly and much more quickly.

<h2 style="border:0;font-weight:bold" align="center">"Help wanted" & "Good first issue"</h2>

<img alt="GitHub issues" src="https://img.shields.io/github/issues/softwareantics/FinalEngine/🏁%20Good%20First%20Issue?color=7057ff&label=Good%20First%20Issues">

We may mark some issues with the label **"good first issue"** which indicates that an issue is straightforward and is a good place to start if you're interested in contributing but are new to the codebase.
