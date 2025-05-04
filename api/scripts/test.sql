SELECT * FROM "Users"
ORDER BY RANDOM()
LIMIT 3;

SELECT * FROM "InvoiceDocuments"
ORDER BY RANDOM()
LIMIT 2;

SELECT * FROM "InvoiceLines"
WHERE "InvoiceId" = (
    SELECT "Id" FROM "InvoiceDocuments"
    ORDER BY RANDOM()
    LIMIT 1
);