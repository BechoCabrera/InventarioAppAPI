IF OBJECT_ID('dbo.InvoicePayments', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.InvoicePayments
    (
        InvoicePaymentId UNIQUEIDENTIFIER NOT NULL
            CONSTRAINT PK_InvoicePayments PRIMARY KEY
            CONSTRAINT DF_InvoicePayments_InvoicePaymentId DEFAULT NEWID(),
        InvoiceId UNIQUEIDENTIFIER NOT NULL,
        PaymentMethod NVARCHAR(50) NOT NULL,
        Amount DECIMAL(18,2) NOT NULL,
        CreatedAt DATETIME NOT NULL
            CONSTRAINT DF_InvoicePayments_CreatedAt DEFAULT GETDATE()
    );

    ALTER TABLE dbo.InvoicePayments
    ADD CONSTRAINT FK_InvoicePayments_Invoices_InvoiceId
        FOREIGN KEY (InvoiceId)
        REFERENCES dbo.Invoices (InvoiceId)
        ON DELETE CASCADE;

    CREATE INDEX IX_InvoicePayments_InvoiceId
        ON dbo.InvoicePayments (InvoiceId);

    CREATE INDEX IX_InvoicePayments_PaymentMethod
        ON dbo.InvoicePayments (PaymentMethod);
END;
GO
