# backlog-system

## Remaining Backlog Endpoint

http://localhost:8000/api/backlog/remaining

In order to provide a more robust system, we need to page the records. Use the following query parameters to page through all the records:

| Param Name | Default Value|
|------------|-------------:|
| startAt    | 0            |
| maxPageSize| 100          |


Make sure that you also provide an authorized user with a valid password in your environment configuration:

```
    "env": {
        "Jira:User": "<username>",
        "Jira:Password": "<password>"
    }
