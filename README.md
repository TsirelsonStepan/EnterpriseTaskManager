## How to deploy locally

1. Set up SECRET_KEY_FOR_JWT and SQLITE_DB_DIR_PATH environment variables. The SECRET_KEY_FOR_JWT is a secret key used to issue JWT for authentication, SQLITE_DB_DIR_PATH points to the dirrectory where the .db file is stored (or where it will be created), make sure that the directory has a permission to create/delete files.
2. Run
```bash
docker compose up taskmanagerapi
```

---

## Documentation

![File structure](Documentation/FileStructure.drawio.svg)
