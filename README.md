# csharp-blog

# Setup
## Database
Postgres setup

```shell
createdb blog

psql blog
```


```sql
CREATE TABLE articles (
	id         uuid PRIMARY KEY,
	created    timestamp,
	updated    timestamp,
	slug       character varying(50),
	title      text,
	content    text
);

CREATE ROLE blog_author PASSWORD 'secret';

GRANT ALL ON articles TO blog_author;
ALTER ROLE blog_author WITH LOGIN;

```

## Get the code
```shell
git clone https://github.com/Rushi98/csharp-blog
cd csharp-blog
```

## Run the code
```shell
dotnet run
```
