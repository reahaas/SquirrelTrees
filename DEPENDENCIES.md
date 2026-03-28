Dependencies and setup

Essential development dependencies:
- Unity Editor: 2022.3.20f1 (see ProjectSettings/ProjectVersion.txt)
- DOTween (Demigiant) — used by the project for tweening. This plugin was removed from the repo to keep the repository lightweight; please install it locally using one of these options:
  1. Import from the Unity Asset Store (search "DOTween (HOTween v2)" by Demigiant) into your project.
  2. If you have a UPM or git package for DOTween, add it to `Packages/manifest.json`.

Recommended steps for a new developer:
1. Install Unity 2022.3.20f1.
2. Clone the repo and checkout the branch you want (e.g., `main`).
3. In Unity, open the project — Unity will create the `Library/` folder.
4. Install DOTween via the Asset Store or package manager.
5. If any other external assets are required, the editor will warn; install them similarly.

If you want me to vendor DOTween into the repo (commit binaries), I can re-add it — otherwise keeping it out and documenting the dependency keeps the repo small and avoids licensing issues.