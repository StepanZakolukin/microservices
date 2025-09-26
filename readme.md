**Проверка работоспособности микросервисов**

Task Service:

```
curl -X GET https://localhost:5002/tasks-service/health
```
NotificationService:

```
curl -X GET https://localhost:5002/notification-service/health
```
**Task Service API**

Создать задачу:
```
curl -X POST https://localhost:5002/api/tasks -H "Content-Type: application/json" -d '{"creatorId": "3fa85f64-5717-4562-b3fc-2c963f66afa6", "title": "Заголовок", "description": "Описание"}'
```
Пример ответа: ```"06798c34-9406-4f73-9705-aff43728677b"```

Назначить исполнителя задачи:
```
curl -X PUT https://localhost:5002/api/tasks/06798c34-9406-4f73-9705-aff43728677b/assign -H "Content-Type: application/json" -d '{"performerId": "42ec3766-08c9-466c-af8a-15109faa10da"}'
```
Обновить задачу:
```
curl -X PUT https://localhost:5002/api/tasks/06798c34-9406-4f73-9705-aff43728677b -H "Content-Type: application/json" -d '{"title": "Новый заголовок","description": "Новое описание"}'
```
Получить задачу по id:
```
curl -X GET https://localhost:5002/api/tasks/06798c34-9406-4f73-9705-aff43728677b
```
Пример ответа:
```
{"id":"06798c34-9406-4f73-9705-aff43728677b","performerId":"42ec3766-08c9-466c-af8a-15109faa10da","creatorId":"3fa85f64-5717-4562-b3fc-2c963f66afa6","title":"Новый заголовок","description":"Новое описание"}
```
Удалить задачу по id (мягкое удаление):
```
curl -X DELETE https://localhost:5002/api/tasks/06798c34-9406-4f73-9705-aff43728677b
```
В результате удаления у задачи будет изменено поле ```Deleted``` на ```true``` и она становится ```readonly```.

Получить отфильтрованный список задач (в данном примере фильтрация по вхождению в поле ```title``` подстроки "Заголовок"):
```
curl -X GET "https://localhost:5002/api/tasks?title=%D0%97%D0%B0%D0%B3%D0%BE%D0%BB%D0%BE%D0%B2%D0%BE%D0%BA&page=1&pageSize=10"
```
Пример ответа:
```
[{"id":"06798c34-9406-4f73-9705-aff43728677b","performerId":"42ec3766-08c9-466c-af8a-15109faa10da","creatorId":"3fa85f64-5717-4562-b3fc-2c963f66afa6","title":"Новый заголовок","description":"Новое описание"},{"id":"42ec3766-08c9-466c-af8a-15109faa10da","performerId":"00000000-0000-0000-0000-000000000000","creatorId":"3fa85f64-5717-4562-b3fc-2c963f66afa6","title":"Заголовок","description":"Описание"}]
```

**Notification Service API**

Создание уведомления:
```
curl -X POST https://localhost:5004/api/notifications -H "Content-Type: application/json" -d '{"userId":"3fa85f64-5717-4562-b3fc-2c963f66afa6","text":"Текст уведомления","taskId":"3fa85f64-5717-4562-b3fc-2c963f66afa6"}'
```
Пример ответа: ```"56c1cd65-0138-469d-8a66-c8f82719a0f8"```

Пометить уведомление как прочитанное:
```
curl -X PUT https://localhost:5004/api/notifications/56c1cd65-0138-469d-8a66-c8f82719a0f8/mark-as-read
```
Получить уведомления пользователя:
```
curl -X GET https://localhost:5004/api/notifications/3fa85f64-5717-4562-b3fc-2c963f66afa6
```
Пример ответа:
```
[{"id":"56c1cd65-0138-469d-8a66-c8f82719a0f8","created":"2025-09-26T15:38:06.490256Z","text":"Текст уведомления","taskId":"3fa85f64-5717-4562-b3fc-2c963f66afa6","readIt":true}]
```
