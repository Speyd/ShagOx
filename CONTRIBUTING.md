# Git Workflow

## Branch Structure

### Main branches

- `main` — stable production-ready version of the project.
- `develop` — main development branch.

---

### Temporary branches

#### New features

```
feature/auth
feature/user-profile
feature/payment
feature/admin-panel
```

#### Bug fixes

```
bugfix/login-validation
bugfix/avatar-upload
```

#### Hotfixes (production critical fixes)

```
hotfix/security-patch
hotfix/payment-error
```

#### Release preparation

```
release/1.0.0
release/1.1.0
release/2.0.0
```

---

## Repository Structure

```
project/
│
├── frontend/
│   ├── src/
│   └── package.json
│
├── backend/
│   ├── src/
│   └── requirements.txt
│
├── infrastructure/
│   ├── docker/
│   └── kubernetes/
│
├── docs/
│
├── .gitignore
└── README.md
```

---

## Development Rules

### Creating a new task

Before starting work:

```bash
git checkout develop
git pull origin develop
git checkout -b feature/auth
```

After finishing work:

```bash
git add .
git commit -m "feat(auth): add login endpoint"
git push origin feature/auth
```

Create Pull Request:

```
feature/auth
      ↓
    develop
```

---

## Restrictions

 Do not push directly to `main`
 Do not use force push on `main`

 Do not use meaningless commits:
fix
update
changes
123
final
final_v2

---

## Conventional Commits

Format:

type(scope): description

Example:

feat(auth): add login endpoint
fix(auth): validate refresh token
refactor(user): extract validation service
docs(api): update authentication documentation

---

## Commit Types

- feat → New functionality
- fix → Bug fix
- refactor → Code refactoring without logic change
- docs → Documentation
- test → Tests
- chore → Technical changes
- style → Formatting
- perf → Performance optimization

---

## Development Process

develop
    │
    ├── feature/auth
    ├── feature/payment
    └── feature/profile

Workflow:

Branch creation → Development → Commit → Push → PR → Code Review → CI/CD → Merge into develop

---

## Hotfix

git checkout main
git checkout -b hotfix/payment-error

hotfix/payment-error → main → develop

---

## Pull Request Rules

- Code compiles
- Tests pass
- No debug code
- Docs updated if needed
- Conventional commits used
- Up to date with develop

---

## Merge Policy

Allowed:
- Squash merge
- Rebase merge

Not allowed:
- Direct push to main
- Merge without review

---

## Branch Protection

main:
- PR required
- 1 approval
- CI required
- No force push

develop:
- PR required
- CI required
- No force push
