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
  [name] varchar(255) NOT NULL,
  [location] varchar(255)
)
GO

CREATE TABLE [skill] (
  [id] integer PRIMARY KEY identity NOT NULL,
  [skill] varchar(255),
  [userId] integer
)
GO

CREATE TABLE [service] (
  [id] integer PRIMARY KEY identity NOT NULL,
  [service] varchar(255),
  [userId] integer
)
GO

CREATE TABLE [assignment] (
  [id] integer PRIMARY KEY identity NOT NULL,
  [description] varchar(255),
  [expiration] date,
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
  [name] varchar(255)
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
INSERT INTO dbo.[user]([name],[location]) VALUES ( '007', 'England')
INSERT INTO dbo.[user]([name],[location]) VALUES ( 'Hariett', 'America')
INSERT INTO dbo.[user]([name],[location]) VALUES ( 'Ethan Hunt', 'America')

INSERT INTO dbo.agency([name]) VALUES ( 'MI6')
INSERT INTO dbo.agency([name]) VALUES ( 'CIA')

INSERT INTO dbo.assignment([description],[expiration],[userId]) VALUES ( 'Steal the Eiffel Tower', '2023-05-12', 1)
INSERT INTO dbo.assignment([description],[expiration],[userId]) VALUES ( 'Steal Intel from the Cartel', '2023-07-08', 2)
INSERT INTO dbo.assignment([description],[expiration],[userId]) VALUES ( 'Assassinate the President of the Moon', '2023-12-24', 3)

INSERT INTO dbo.skill( [skill], [userId]) VALUES ( 'Marksman', 1)
INSERT INTO dbo.skill( [skill], [userId]) VALUES ( 'Espionage', 1)
INSERT INTO dbo.skill( [skill], [userId]) VALUES ( 'Spycraft', 1)
INSERT INTO dbo.skill( [skill], [userId]) VALUES ( 'Problem Solving', 2)
INSERT INTO dbo.skill( [skill], [userId]) VALUES ( 'Disguises', 3)
INSERT INTO dbo.skill( [skill], [userId]) VALUES ( 'Interrogation', 3)

INSERT INTO dbo.[service]( [service], [userId]) VALUES ( 'Spying', 1)
INSERT INTO dbo.[service]( [service], [userId]) VALUES ( 'Investigations', 2)
INSERT INTO dbo.[service]( [service], [userId]) VALUES ( 'Infiltration', 3)

INSERT INTO affiliation([agencyId],[userId]) VALUES ( 1, 1)
INSERT INTO affiliation([agencyId],[userId]) VALUES ( 2, 2)
INSERT INTO affiliation([agencyId],[userId]) VALUES ( 1, 2)

INSERT INTO dbo.foe([userId],[foeId]) VALUES ( 2, 3)

INSERT INTO dbo.friend([userId],[friendId]) VALUES ( 1, 2)




