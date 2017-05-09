# backlog-system

## Backlog System UI

The user interface is an angular application (angular cli). Use the following command to start it:
```
ng serve
```

## Backlog System API

### Endpoint documentation

A full documentation of the provided endpoints can be found here: 

```
http://localhost:8000/swagger/
```

### Configuration

Make sure that you also provide an authorized user with a valid password in your environment configuration:

```
    "env": {
        "Jira:User": "<username>",
        "Jira:Password": "<password>"
    }
```
