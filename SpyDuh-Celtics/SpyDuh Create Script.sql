USE [master]
GO
IF DB_ID('SpyDuh') IS NULL
	CREATE DATABASE [SpyDuh]
GO
USE [SpyDuh]
GO

DROP TABLE IF EXISTS [user];
DROP TABLE IF EXISTS [skill];
DROP TABLE IF EXISTS [service];
DROP TABLE IF EXISTS [assignment];
DROP TABLE IF EXISTS [friend];
DROP TABLE IF EXISTS [foe];
DROP TABLE IF EXISTS [agency];
DROP TABLE IF EXISTS [affiliation];


CREATE TABLE [user] (
  [id] integer PRIMARY KEY identity NOT NULL,
  [name] varchar NOT NULL,
  [location] varchar
)
GO

CREATE TABLE [skill] (
  [id] integer PRIMARY KEY identity NOT NULL,
  [skill] varchar,
  [userId] integer
)
GO

CREATE TABLE [service] (
  [id] integer PRIMARY KEY identity NOT NULL,
  [service] varchar,
  [userId] integer
)
GO

CREATE TABLE [assignment] (
  [id] integer PRIMARY KEY identity NOT NULL,
  [description] varchar,
  [experation] date,
  [userId] integer
)
GO

CREATE TABLE [friend] (
  [id] integer PRIMARY KEY identity NOT NULL,
  [userId] integer,
  [friendId] integer
)
GO

CREATE TABLE [foe] (
  [id] integer PRIMARY KEY identity NOT NULL,
  [userId] integer,
  [foeId] integer
)
GO

CREATE TABLE [agency] (
  [id] integer PRIMARY KEY identity NOT NULL,
  [name] varchar
)
GO

CREATE TABLE [affiliation] (
  [id] integer PRIMARY KEY identity NOT NULL,
  [agencyId] integer,
  [userId] integer
)
GO

ALTER TABLE [friend] ADD FOREIGN KEY ([userId]) REFERENCES [user] ([id])
GO

ALTER TABLE [friend] ADD FOREIGN KEY ([friendId]) REFERENCES [user] ([id])
GO

ALTER TABLE [foe] ADD FOREIGN KEY ([userId]) REFERENCES [user] ([id])
GO

ALTER TABLE [foe] ADD FOREIGN KEY ([foeId]) REFERENCES [user] ([id])
GO

ALTER TABLE [skill] ADD FOREIGN KEY ([userId]) REFERENCES [user] ([id])
GO

ALTER TABLE [service] ADD FOREIGN KEY ([userId]) REFERENCES [user] ([id])
GO

ALTER TABLE [assignment] ADD FOREIGN KEY ([userId]) REFERENCES [user] ([id])
GO

ALTER TABLE [affiliation] ADD FOREIGN KEY ([agencyId]) REFERENCES [agency] ([id])
GO

ALTER TABLE [affiliation] ADD FOREIGN KEY ([userId]) REFERENCES [user] ([id])
GO
