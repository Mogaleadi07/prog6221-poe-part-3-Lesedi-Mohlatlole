-- using the insert opertation
INSERT INTO dbo.TaskHelper (Title, Description, ReminderDate, IsCompleted)
VALUES ('Buy groceries', 'Milk, Bread, Eggs', '2024-07-01 10:00:00', 0),
       ('Submit assignement','programming assignment due','2024-07-02 23:59:00',0),
       ('Call mom','Check in with mom','2024-07-03 18:00:00',0);
-- using the select operation
SELECT * FROM dbo.TaskHelper;

-- using the update operation
UPDATE dbo.TaskHelper
SET IsCompleted = 1
WHERE Id = 1;
--using the delete operation
DELETE FROM dbo.TaskHelper
WHERE Id = 1;