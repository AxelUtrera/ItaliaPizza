
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/09/2023 13:52:41
-- Generated from EDMX file: C:\Users\danie\OneDrive\Documentos\Universidad\8 Semestre\Desarrollo de software\ItaliaPizza\ItaliaPizza\DataAccess\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ItaliaPizza];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__address__idCusto__52593CB8]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[address] DROP CONSTRAINT [FK__address__idCusto__52593CB8];
GO
IF OBJECT_ID(N'[dbo].[FK_deliveryOrder_address]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[deliveryOrder] DROP CONSTRAINT [FK_deliveryOrder_address];
GO
IF OBJECT_ID(N'[dbo].[FK__dailyBala__idCas__5535A963]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[dailyBalance] DROP CONSTRAINT [FK__dailyBala__idCas__5535A963];
GO
IF OBJECT_ID(N'[dbo].[FK__customer__idUser__534D60F1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[customer] DROP CONSTRAINT [FK__customer__idUser__534D60F1];
GO
IF OBJECT_ID(N'[dbo].[FK_deliveryOrder_customer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[deliveryOrder] DROP CONSTRAINT [FK_deliveryOrder_customer];
GO
IF OBJECT_ID(N'[dbo].[FK__dailyBala__idWor__5441852A]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[dailyBalance] DROP CONSTRAINT [FK__dailyBala__idWor__5441852A];
GO
IF OBJECT_ID(N'[dbo].[FK_deliveryOrder_orders]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[deliveryOrder] DROP CONSTRAINT [FK_deliveryOrder_orders];
GO
IF OBJECT_ID(N'[dbo].[FK__recipeIng__idIng__5CD6CB2B]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[recipeIngredient] DROP CONSTRAINT [FK__recipeIng__idIng__5CD6CB2B];
GO
IF OBJECT_ID(N'[dbo].[FK__supplierI__idIng__5FB337D6]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[supplierIngredient] DROP CONSTRAINT [FK__supplierI__idIng__5FB337D6];
GO
IF OBJECT_ID(N'[dbo].[FK__orders__idWorker__5AEE82B9]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[orders] DROP CONSTRAINT [FK__orders__idWorker__5AEE82B9];
GO
IF OBJECT_ID(N'[dbo].[FK_orderProduct_orders]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[orderProduct] DROP CONSTRAINT [FK_orderProduct_orders];
GO
IF OBJECT_ID(N'[dbo].[FK__product__idRecip__6E01572D]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[product] DROP CONSTRAINT [FK__product__idRecip__6E01572D];
GO
IF OBJECT_ID(N'[dbo].[FK__supplierP__produ__73BA3083]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[supplierProduct] DROP CONSTRAINT [FK__supplierP__produ__73BA3083];
GO
IF OBJECT_ID(N'[dbo].[FK_orderProduct_product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[orderProduct] DROP CONSTRAINT [FK_orderProduct_product];
GO
IF OBJECT_ID(N'[dbo].[FK__recipeIng__idRec__5DCAEF64]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[recipeIngredient] DROP CONSTRAINT [FK__recipeIng__idRec__5DCAEF64];
GO
IF OBJECT_ID(N'[dbo].[FK__supplierI__idSup__5EBF139D]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[supplierIngredient] DROP CONSTRAINT [FK__supplierI__idSup__5EBF139D];
GO
IF OBJECT_ID(N'[dbo].[FK__supplierP__idSup__60A75C0F]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[supplierProduct] DROP CONSTRAINT [FK__supplierP__idSup__60A75C0F];
GO
IF OBJECT_ID(N'[dbo].[FK__worker__idUser__628FA481]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[worker] DROP CONSTRAINT [FK__worker__idUser__628FA481];
GO
IF OBJECT_ID(N'[dbo].[FK_supplierOrdersupplier]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[supplierOrder] DROP CONSTRAINT [FK_supplierOrdersupplier];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[address]', 'U') IS NOT NULL
    DROP TABLE [dbo].[address];
GO
IF OBJECT_ID(N'[dbo].[cashbox]', 'U') IS NOT NULL
    DROP TABLE [dbo].[cashbox];
GO
IF OBJECT_ID(N'[dbo].[customer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[customer];
GO
IF OBJECT_ID(N'[dbo].[dailyBalance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[dailyBalance];
GO
IF OBJECT_ID(N'[dbo].[deliveryOrder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[deliveryOrder];
GO
IF OBJECT_ID(N'[dbo].[ingredient]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ingredient];
GO
IF OBJECT_ID(N'[dbo].[orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[orders];
GO
IF OBJECT_ID(N'[dbo].[product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[product];
GO
IF OBJECT_ID(N'[dbo].[recipe]', 'U') IS NOT NULL
    DROP TABLE [dbo].[recipe];
GO
IF OBJECT_ID(N'[dbo].[recipeIngredient]', 'U') IS NOT NULL
    DROP TABLE [dbo].[recipeIngredient];
GO
IF OBJECT_ID(N'[dbo].[supplier]', 'U') IS NOT NULL
    DROP TABLE [dbo].[supplier];
GO
IF OBJECT_ID(N'[dbo].[supplierOrder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[supplierOrder];
GO
IF OBJECT_ID(N'[dbo].[users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[users];
GO
IF OBJECT_ID(N'[dbo].[worker]', 'U') IS NOT NULL
    DROP TABLE [dbo].[worker];
GO
IF OBJECT_ID(N'[dbo].[orderProduct]', 'U') IS NOT NULL
    DROP TABLE [dbo].[orderProduct];
GO
IF OBJECT_ID(N'[dbo].[supplierIngredient]', 'U') IS NOT NULL
    DROP TABLE [dbo].[supplierIngredient];
GO
IF OBJECT_ID(N'[dbo].[supplierProduct]', 'U') IS NOT NULL
    DROP TABLE [dbo].[supplierProduct];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'address'
CREATE TABLE [dbo].[address] (
    [idAddress] int IDENTITY(1,1) NOT NULL,
    [street] varchar(40)  NOT NULL,
    [number] varchar(5)  NOT NULL,
    [city] varchar(30)  NOT NULL,
    [zipcode] varchar(5)  NOT NULL,
    [neighborhood] varchar(30)  NOT NULL,
    [instructions] varchar(100)  NOT NULL,
    [idCustomer] int  NOT NULL
);
GO

-- Creating table 'cashbox'
CREATE TABLE [dbo].[cashbox] (
    [idCashbox] int IDENTITY(1,1) NOT NULL,
    [incomes] float  NOT NULL,
    [outcomes] float  NOT NULL,
    [totalAmount] float  NOT NULL
);
GO

-- Creating table 'customer'
CREATE TABLE [dbo].[customer] (
    [idCustomer] int IDENTITY(1,1) NOT NULL,
    [idUser] int  NOT NULL
);
GO

-- Creating table 'dailyBalance'
CREATE TABLE [dbo].[dailyBalance] (
    [idDailyBalance] int IDENTITY(1,1) NOT NULL,
    [date] varchar(10)  NOT NULL,
    [totalMoney] float  NOT NULL,
    [idWorker] varchar(16)  NOT NULL,
    [idCashbox] int  NOT NULL
);
GO

-- Creating table 'deliveryOrder'
CREATE TABLE [dbo].[deliveryOrder] (
    [idDeliveryOrder] int IDENTITY(1,1) NOT NULL,
    [idOrder] int  NOT NULL,
    [idCustomer] int  NOT NULL,
    [idAddress] int  NOT NULL
);
GO

-- Creating table 'ingredient'
CREATE TABLE [dbo].[ingredient] (
    [idIngredient] int IDENTITY(1,1) NOT NULL,
    [ingredientName] varchar(30)  NOT NULL,
    [quantity] float  NOT NULL,
    [active] bit  NOT NULL
);
GO

-- Creating table 'orders'
CREATE TABLE [dbo].[orders] (
    [idOrder] int IDENTITY(1,1) NOT NULL,
    [date] varchar(10)  NOT NULL,
    [hour] varchar(10)  NOT NULL,
    [status] varchar(15)  NULL,
    [idWorker] varchar(16)  NOT NULL,
    [typeOrder] varchar(17)  NULL
);
GO

-- Creating table 'product'
CREATE TABLE [dbo].[product] (
    [productCode] varchar(5)  NOT NULL,
    [description] varchar(150)  NOT NULL,
    [picture] varbinary(max)  NOT NULL,
    [price] float  NOT NULL,
    [preparation] bit  NOT NULL,
    [productName] varchar(50)  NOT NULL,
    [restrictions] varchar(150)  NOT NULL,
    [idRecipe] int  NOT NULL,
    [active] bit  NOT NULL,
    [quantity] float  NOT NULL
);
GO

-- Creating table 'recipe'
CREATE TABLE [dbo].[recipe] (
    [idRecipe] int IDENTITY(1,1) NOT NULL,
    [description] varchar(150)  NOT NULL,
    [recipeName] varchar(50)  NOT NULL,
    [active] bit  NOT NULL
);
GO

-- Creating table 'recipeIngredient'
CREATE TABLE [dbo].[recipeIngredient] (
    [idRecipeIngredient] int IDENTITY(1,1) NOT NULL,
    [idIngredient] int  NOT NULL,
    [idRecipe] int  NOT NULL,
    [quantity] int  NOT NULL
);
GO

-- Creating table 'supplier'
CREATE TABLE [dbo].[supplier] (
    [idSupplier] int IDENTITY(1,1) NOT NULL,
    [supplierAddress] varchar(50)  NOT NULL,
    [email] varchar(50)  NOT NULL,
    [phoneNumber] varchar(10)  NOT NULL,
    [rfc] varchar(13)  NOT NULL,
    [supplierType] varchar(20)  NOT NULL,
    [supplierName] varchar(50)  NOT NULL,
    [active] bit  NOT NULL
);
GO

-- Creating table 'supplierOrder'
CREATE TABLE [dbo].[supplierOrder] (
    [orderNumber] varchar(5)  NOT NULL,
    [arriveDate] datetime  NOT NULL,
    [orderDate] datetime  NOT NULL,
    [status] varchar(20)  NOT NULL,
    [idSupplier] int  NOT NULL
);
GO

-- Creating table 'users'
CREATE TABLE [dbo].[users] (
    [idUser] int IDENTITY(1,1) NOT NULL,
    [email] varchar(50)  NOT NULL,
    [name] varchar(50)  NOT NULL,
    [lastname] varchar(50)  NOT NULL,
    [phoneNumber] varchar(10)  NOT NULL,
    [active] bit  NOT NULL
);
GO

-- Creating table 'worker'
CREATE TABLE [dbo].[worker] (
    [username] varchar(16)  NOT NULL,
    [nss] varchar(11)  NOT NULL,
    [password] varchar(64)  NOT NULL,
    [rfc] varchar(13)  NOT NULL,
    [role] varchar(13)  NOT NULL,
    [workerNumber] varchar(5)  NOT NULL,
    [idUser] int  NOT NULL
);
GO

-- Creating table 'orderProduct'
CREATE TABLE [dbo].[orderProduct] (
    [idOrder] int  NOT NULL,
    [idProduct] varchar(5)  NOT NULL,
    [quantity] int  NOT NULL
);
GO

-- Creating table 'supplierIngredient'
CREATE TABLE [dbo].[supplierIngredient] (
    [idSupplierOrder] varchar(5)  NOT NULL,
    [idIngredient] int  NOT NULL,
    [quantity] float  NOT NULL
);
GO

-- Creating table 'supplierProduct'
CREATE TABLE [dbo].[supplierProduct] (
    [idSupplierOrder] varchar(5)  NOT NULL,
    [productCode] varchar(5)  NOT NULL,
    [quantity] float  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [idAddress] in table 'address'
ALTER TABLE [dbo].[address]
ADD CONSTRAINT [PK_address]
    PRIMARY KEY CLUSTERED ([idAddress] ASC);
GO

-- Creating primary key on [idCashbox] in table 'cashbox'
ALTER TABLE [dbo].[cashbox]
ADD CONSTRAINT [PK_cashbox]
    PRIMARY KEY CLUSTERED ([idCashbox] ASC);
GO

-- Creating primary key on [idCustomer] in table 'customer'
ALTER TABLE [dbo].[customer]
ADD CONSTRAINT [PK_customer]
    PRIMARY KEY CLUSTERED ([idCustomer] ASC);
GO

-- Creating primary key on [idDailyBalance] in table 'dailyBalance'
ALTER TABLE [dbo].[dailyBalance]
ADD CONSTRAINT [PK_dailyBalance]
    PRIMARY KEY CLUSTERED ([idDailyBalance] ASC);
GO

-- Creating primary key on [idDeliveryOrder] in table 'deliveryOrder'
ALTER TABLE [dbo].[deliveryOrder]
ADD CONSTRAINT [PK_deliveryOrder]
    PRIMARY KEY CLUSTERED ([idDeliveryOrder] ASC);
GO

-- Creating primary key on [idIngredient] in table 'ingredient'
ALTER TABLE [dbo].[ingredient]
ADD CONSTRAINT [PK_ingredient]
    PRIMARY KEY CLUSTERED ([idIngredient] ASC);
GO

-- Creating primary key on [idOrder] in table 'orders'
ALTER TABLE [dbo].[orders]
ADD CONSTRAINT [PK_orders]
    PRIMARY KEY CLUSTERED ([idOrder] ASC);
GO

-- Creating primary key on [productCode] in table 'product'
ALTER TABLE [dbo].[product]
ADD CONSTRAINT [PK_product]
    PRIMARY KEY CLUSTERED ([productCode] ASC);
GO

-- Creating primary key on [idRecipe] in table 'recipe'
ALTER TABLE [dbo].[recipe]
ADD CONSTRAINT [PK_recipe]
    PRIMARY KEY CLUSTERED ([idRecipe] ASC);
GO

-- Creating primary key on [idRecipeIngredient] in table 'recipeIngredient'
ALTER TABLE [dbo].[recipeIngredient]
ADD CONSTRAINT [PK_recipeIngredient]
    PRIMARY KEY CLUSTERED ([idRecipeIngredient] ASC);
GO

-- Creating primary key on [idSupplier] in table 'supplier'
ALTER TABLE [dbo].[supplier]
ADD CONSTRAINT [PK_supplier]
    PRIMARY KEY CLUSTERED ([idSupplier] ASC);
GO

-- Creating primary key on [orderNumber] in table 'supplierOrder'
ALTER TABLE [dbo].[supplierOrder]
ADD CONSTRAINT [PK_supplierOrder]
    PRIMARY KEY CLUSTERED ([orderNumber] ASC);
GO

-- Creating primary key on [idUser] in table 'users'
ALTER TABLE [dbo].[users]
ADD CONSTRAINT [PK_users]
    PRIMARY KEY CLUSTERED ([idUser] ASC);
GO

-- Creating primary key on [username] in table 'worker'
ALTER TABLE [dbo].[worker]
ADD CONSTRAINT [PK_worker]
    PRIMARY KEY CLUSTERED ([username] ASC);
GO

-- Creating primary key on [idOrder], [idProduct], [quantity] in table 'orderProduct'
ALTER TABLE [dbo].[orderProduct]
ADD CONSTRAINT [PK_orderProduct]
    PRIMARY KEY CLUSTERED ([idOrder], [idProduct], [quantity] ASC);
GO

-- Creating primary key on [idSupplierOrder], [idIngredient], [quantity] in table 'supplierIngredient'
ALTER TABLE [dbo].[supplierIngredient]
ADD CONSTRAINT [PK_supplierIngredient]
    PRIMARY KEY CLUSTERED ([idSupplierOrder], [idIngredient], [quantity] ASC);
GO

-- Creating primary key on [idSupplierOrder], [productCode], [quantity] in table 'supplierProduct'
ALTER TABLE [dbo].[supplierProduct]
ADD CONSTRAINT [PK_supplierProduct]
    PRIMARY KEY CLUSTERED ([idSupplierOrder], [productCode], [quantity] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [idCustomer] in table 'address'
ALTER TABLE [dbo].[address]
ADD CONSTRAINT [FK__address__idCusto__52593CB8]
    FOREIGN KEY ([idCustomer])
    REFERENCES [dbo].[customer]
        ([idCustomer])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__address__idCusto__52593CB8'
CREATE INDEX [IX_FK__address__idCusto__52593CB8]
ON [dbo].[address]
    ([idCustomer]);
GO

-- Creating foreign key on [idAddress] in table 'deliveryOrder'
ALTER TABLE [dbo].[deliveryOrder]
ADD CONSTRAINT [FK_deliveryOrder_address]
    FOREIGN KEY ([idAddress])
    REFERENCES [dbo].[address]
        ([idAddress])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_deliveryOrder_address'
CREATE INDEX [IX_FK_deliveryOrder_address]
ON [dbo].[deliveryOrder]
    ([idAddress]);
GO

-- Creating foreign key on [idCashbox] in table 'dailyBalance'
ALTER TABLE [dbo].[dailyBalance]
ADD CONSTRAINT [FK__dailyBala__idCas__5535A963]
    FOREIGN KEY ([idCashbox])
    REFERENCES [dbo].[cashbox]
        ([idCashbox])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__dailyBala__idCas__5535A963'
CREATE INDEX [IX_FK__dailyBala__idCas__5535A963]
ON [dbo].[dailyBalance]
    ([idCashbox]);
GO

-- Creating foreign key on [idUser] in table 'customer'
ALTER TABLE [dbo].[customer]
ADD CONSTRAINT [FK__customer__idUser__534D60F1]
    FOREIGN KEY ([idUser])
    REFERENCES [dbo].[users]
        ([idUser])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__customer__idUser__534D60F1'
CREATE INDEX [IX_FK__customer__idUser__534D60F1]
ON [dbo].[customer]
    ([idUser]);
GO

-- Creating foreign key on [idCustomer] in table 'deliveryOrder'
ALTER TABLE [dbo].[deliveryOrder]
ADD CONSTRAINT [FK_deliveryOrder_customer]
    FOREIGN KEY ([idCustomer])
    REFERENCES [dbo].[customer]
        ([idCustomer])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_deliveryOrder_customer'
CREATE INDEX [IX_FK_deliveryOrder_customer]
ON [dbo].[deliveryOrder]
    ([idCustomer]);
GO

-- Creating foreign key on [idWorker] in table 'dailyBalance'
ALTER TABLE [dbo].[dailyBalance]
ADD CONSTRAINT [FK__dailyBala__idWor__5441852A]
    FOREIGN KEY ([idWorker])
    REFERENCES [dbo].[worker]
        ([username])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__dailyBala__idWor__5441852A'
CREATE INDEX [IX_FK__dailyBala__idWor__5441852A]
ON [dbo].[dailyBalance]
    ([idWorker]);
GO

-- Creating foreign key on [idOrder] in table 'deliveryOrder'
ALTER TABLE [dbo].[deliveryOrder]
ADD CONSTRAINT [FK_deliveryOrder_orders]
    FOREIGN KEY ([idOrder])
    REFERENCES [dbo].[orders]
        ([idOrder])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_deliveryOrder_orders'
CREATE INDEX [IX_FK_deliveryOrder_orders]
ON [dbo].[deliveryOrder]
    ([idOrder]);
GO

-- Creating foreign key on [idIngredient] in table 'recipeIngredient'
ALTER TABLE [dbo].[recipeIngredient]
ADD CONSTRAINT [FK__recipeIng__idIng__5CD6CB2B]
    FOREIGN KEY ([idIngredient])
    REFERENCES [dbo].[ingredient]
        ([idIngredient])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__recipeIng__idIng__5CD6CB2B'
CREATE INDEX [IX_FK__recipeIng__idIng__5CD6CB2B]
ON [dbo].[recipeIngredient]
    ([idIngredient]);
GO

-- Creating foreign key on [idIngredient] in table 'supplierIngredient'
ALTER TABLE [dbo].[supplierIngredient]
ADD CONSTRAINT [FK__supplierI__idIng__5FB337D6]
    FOREIGN KEY ([idIngredient])
    REFERENCES [dbo].[ingredient]
        ([idIngredient])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__supplierI__idIng__5FB337D6'
CREATE INDEX [IX_FK__supplierI__idIng__5FB337D6]
ON [dbo].[supplierIngredient]
    ([idIngredient]);
GO

-- Creating foreign key on [idWorker] in table 'orders'
ALTER TABLE [dbo].[orders]
ADD CONSTRAINT [FK__orders__idWorker__5AEE82B9]
    FOREIGN KEY ([idWorker])
    REFERENCES [dbo].[worker]
        ([username])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__orders__idWorker__5AEE82B9'
CREATE INDEX [IX_FK__orders__idWorker__5AEE82B9]
ON [dbo].[orders]
    ([idWorker]);
GO

-- Creating foreign key on [idOrder] in table 'orderProduct'
ALTER TABLE [dbo].[orderProduct]
ADD CONSTRAINT [FK_orderProduct_orders]
    FOREIGN KEY ([idOrder])
    REFERENCES [dbo].[orders]
        ([idOrder])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [idRecipe] in table 'product'
ALTER TABLE [dbo].[product]
ADD CONSTRAINT [FK__product__idRecip__6E01572D]
    FOREIGN KEY ([idRecipe])
    REFERENCES [dbo].[recipe]
        ([idRecipe])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__product__idRecip__6E01572D'
CREATE INDEX [IX_FK__product__idRecip__6E01572D]
ON [dbo].[product]
    ([idRecipe]);
GO

-- Creating foreign key on [productCode] in table 'supplierProduct'
ALTER TABLE [dbo].[supplierProduct]
ADD CONSTRAINT [FK__supplierP__produ__73BA3083]
    FOREIGN KEY ([productCode])
    REFERENCES [dbo].[product]
        ([productCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__supplierP__produ__73BA3083'
CREATE INDEX [IX_FK__supplierP__produ__73BA3083]
ON [dbo].[supplierProduct]
    ([productCode]);
GO

-- Creating foreign key on [idProduct] in table 'orderProduct'
ALTER TABLE [dbo].[orderProduct]
ADD CONSTRAINT [FK_orderProduct_product]
    FOREIGN KEY ([idProduct])
    REFERENCES [dbo].[product]
        ([productCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_orderProduct_product'
CREATE INDEX [IX_FK_orderProduct_product]
ON [dbo].[orderProduct]
    ([idProduct]);
GO

-- Creating foreign key on [idRecipe] in table 'recipeIngredient'
ALTER TABLE [dbo].[recipeIngredient]
ADD CONSTRAINT [FK__recipeIng__idRec__5DCAEF64]
    FOREIGN KEY ([idRecipe])
    REFERENCES [dbo].[recipe]
        ([idRecipe])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__recipeIng__idRec__5DCAEF64'
CREATE INDEX [IX_FK__recipeIng__idRec__5DCAEF64]
ON [dbo].[recipeIngredient]
    ([idRecipe]);
GO

-- Creating foreign key on [idSupplierOrder] in table 'supplierIngredient'
ALTER TABLE [dbo].[supplierIngredient]
ADD CONSTRAINT [FK__supplierI__idSup__5EBF139D]
    FOREIGN KEY ([idSupplierOrder])
    REFERENCES [dbo].[supplierOrder]
        ([orderNumber])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [idSupplierOrder] in table 'supplierProduct'
ALTER TABLE [dbo].[supplierProduct]
ADD CONSTRAINT [FK__supplierP__idSup__60A75C0F]
    FOREIGN KEY ([idSupplierOrder])
    REFERENCES [dbo].[supplierOrder]
        ([orderNumber])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [idUser] in table 'worker'
ALTER TABLE [dbo].[worker]
ADD CONSTRAINT [FK__worker__idUser__628FA481]
    FOREIGN KEY ([idUser])
    REFERENCES [dbo].[users]
        ([idUser])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__worker__idUser__628FA481'
CREATE INDEX [IX_FK__worker__idUser__628FA481]
ON [dbo].[worker]
    ([idUser]);
GO

-- Creating foreign key on [idSupplier] in table 'supplierOrder'
ALTER TABLE [dbo].[supplierOrder]
ADD CONSTRAINT [FK_supplierOrdersupplier]
    FOREIGN KEY ([idSupplier])
    REFERENCES [dbo].[supplier]
        ([idSupplier])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_supplierOrdersupplier'
CREATE INDEX [IX_FK_supplierOrdersupplier]
ON [dbo].[supplierOrder]
    ([idSupplier]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------