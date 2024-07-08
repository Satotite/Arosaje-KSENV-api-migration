BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Membre" (
	"Id_Utilisateur"	INT,
	PRIMARY KEY("Id_Utilisateur"),
	FOREIGN KEY("Id_Utilisateur") REFERENCES "Utilisateur"("Id_Utilisateur")
);
CREATE TABLE IF NOT EXISTS "Proprio" (
	"Id_Utilisateur"	INT,
	PRIMARY KEY("Id_Utilisateur"),
	FOREIGN KEY("Id_Utilisateur") REFERENCES "Utilisateur"("Id_Utilisateur")
);
CREATE TABLE IF NOT EXISTS "Prendre" (
	"Id_Utilisateur"	INT,
	"Id_Photo"	INT,
	PRIMARY KEY("Id_Utilisateur","Id_Photo"),
	FOREIGN KEY("Id_Utilisateur") REFERENCES "Membre"("Id_Utilisateur"),
	FOREIGN KEY("Id_Photo") REFERENCES "Photo"("Id_Photo")
);
CREATE TABLE IF NOT EXISTS "Envoyer_recevoir" (
	"Id_Utilisateur"	INT,
	"Id_Utilisateur_1"	INT,
	"Id_Utilisateur_2"	INT,
	"Id_Message"	INT,
	PRIMARY KEY("Id_Utilisateur","Id_Utilisateur_1","Id_Utilisateur_2","Id_Message"),
	FOREIGN KEY("Id_Utilisateur_2") REFERENCES "Proprio"("Id_Utilisateur"),
	FOREIGN KEY("Id_Utilisateur") REFERENCES "Membre"("Id_Utilisateur"),
	FOREIGN KEY("Id_Utilisateur_1") REFERENCES "Botaniste"("Id_Utilisateur"),
	FOREIGN KEY("Id_Message") REFERENCES "Message"("Id_Message")
);
CREATE TABLE IF NOT EXISTS "Conseiller" (
	"Id_Utilisateur"	INT,
	"Id_Tips"	INT,
	PRIMARY KEY("Id_Utilisateur","Id_Tips"),
	FOREIGN KEY("Id_Utilisateur") REFERENCES "Botaniste"("Id_Utilisateur"),
	FOREIGN KEY("Id_Tips") REFERENCES "dateTips"("Id_Tips")
);
CREATE TABLE IF NOT EXISTS "HasTips" (
	"Id_Plante"	INT,
	"Id_Tips"	INT,
	PRIMARY KEY("Id_Plante","Id_Tips"),
	FOREIGN KEY("Id_Plante") REFERENCES "Plante"("Id_Plante"),
	FOREIGN KEY("Id_Tips") REFERENCES "dateTips"("Id_Tips")
);
CREATE TABLE IF NOT EXISTS "Habite" (
	"Id_Utilisateur"	INT,
	"Id_Ville"	INT,
	PRIMARY KEY("Id_Utilisateur","Id_Ville"),
	FOREIGN KEY("Id_Ville") REFERENCES "Ville"("Id_Ville"),
	FOREIGN KEY("Id_Utilisateur") REFERENCES "Utilisateur"("Id_Utilisateur")
);
CREATE TABLE IF NOT EXISTS "Botaniste" (
	"Id_Utilisateur"	INTEGER,
	PRIMARY KEY("Id_Utilisateur"),
	FOREIGN KEY("Id_Utilisateur") REFERENCES "Utilisateur"("Id_Utilisateur")
);
CREATE TABLE IF NOT EXISTS "Message" (
	"Id_Message"	INTEGER UNIQUE,
	"contenu"	VARCHAR(50),
	"dateMessage"	DATE,
	PRIMARY KEY("Id_Message" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Photo" (
	"Id_Photo"	INTEGER UNIQUE,
	"image"	VARCHAR(50),
	PRIMARY KEY("Id_Photo" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Plante" (
	"Id_Plante"	INTEGER UNIQUE,
	"Espece"	VARCHAR(50),
	"Description"	VARCHAR(500),
	"Categorie"	VARCHAR(50),
	"Etat"	VARCHAR(50),
	"Nom"	VARCHAR(50),
	"Id_Ville"	INT NOT NULL,
	"Lon"	TEXT NOT NULL,
	"Lat"	INTEGER NOT NULL,
	"Id_Photo"	INT,
	"Id_Utilisateur"	INT NOT NULL,
	"Id_Utilisateur_1"	INT NOT NULL,
	PRIMARY KEY("Id_Plante" AUTOINCREMENT),
	FOREIGN KEY("Id_Utilisateur") REFERENCES "Membre"("Id_Utilisateur"),
	FOREIGN KEY("Id_Utilisateur_1") REFERENCES "Membre"("Id_Utilisateur"),
	FOREIGN KEY("Id_Ville") REFERENCES "Ville"("Id_Ville"),
	FOREIGN KEY("Id_Photo") REFERENCES "Photo"("Id_Photo")
);
CREATE TABLE IF NOT EXISTS "Utilisateur" (
	"Id_Utilisateur"	INTEGER UNIQUE,
	"Nom"	VARCHAR(50),
	"Mdp"	TEXT,
	"Email"	VARCHAR(50),
	"Prenom"	VARCHAR(50),
	"Age"	varchar(3),
	PRIMARY KEY("Id_Utilisateur" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Ville" (
	"Id_Ville"	INTEGER UNIQUE,
	"Nom"	VARCHAR(50),
	"Desc"	VARCHAR(500),
	PRIMARY KEY("Id_Ville" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "dateTips" (
	"Id_Tips"	INTEGER UNIQUE,
	"contenu"	VARCHAR(50),
	PRIMARY KEY("Id_Tips" AUTOINCREMENT)
);
INSERT INTO "Membre" ("Id_Utilisateur") VALUES (1);
INSERT INTO "Membre" ("Id_Utilisateur") VALUES (2);
INSERT INTO "Proprio" ("Id_Utilisateur") VALUES (1);
INSERT INTO "Conseiller" ("Id_Utilisateur","Id_Tips") VALUES (2,1);
INSERT INTO "Conseiller" ("Id_Utilisateur","Id_Tips") VALUES (2,2);
INSERT INTO "HasTips" ("Id_Plante","Id_Tips") VALUES (1,1);
INSERT INTO "HasTips" ("Id_Plante","Id_Tips") VALUES (2,2);
INSERT INTO "Habite" ("Id_Utilisateur","Id_Ville") VALUES (1,1);
INSERT INTO "Habite" ("Id_Utilisateur","Id_Ville") VALUES (2,1);
INSERT INTO "Botaniste" ("Id_Utilisateur") VALUES (2);
INSERT INTO "Message" ("Id_Message","contenu","dateMessage") VALUES (1,'Bonjour','2024-03-07');
INSERT INTO "Message" ("Id_Message","contenu","dateMessage") VALUES (2,'Comment ça va ?','2024-03-07');
INSERT INTO "Photo" ("Id_Photo","image") VALUES (1,'image1.jpg');
INSERT INTO "Photo" ("Id_Photo","image") VALUES (2,'image2.jpg');
INSERT INTO "Plante" ("Id_Plante","Espece","Description","Categorie","Etat","Nom","Id_Ville","Lon","Lat","Id_Photo","Id_Utilisateur","Id_Utilisateur_1") VALUES (1,'Rose','Belle fleur rouge','Fleur','En bonne santé','Rose rouge',1,'45.758888',4.841389,1,1,1);
INSERT INTO "Plante" ("Id_Plante","Espece","Description","Categorie","Etat","Nom","Id_Ville","Lon","Lat","Id_Photo","Id_Utilisateur","Id_Utilisateur_1") VALUES (2,'Tulipe','Fleur printanière','Fleur','En bonne santé','Tulipe rouge',1,'45.758888',4.841389,2,2,2);
INSERT INTO "Utilisateur" ("Id_Utilisateur","Nom","Mdp","Email","Prenom","Age") VALUES (1,'Dupont','mdp123','dupont@example.com','Jean','30');
INSERT INTO "Utilisateur" ("Id_Utilisateur","Nom","Mdp","Email","Prenom","Age") VALUES (2,'Durand','azerty','durand@example.com','Marie','25');
INSERT INTO "Ville" ("Id_Ville","Nom","Desc") VALUES (1,'Lyon','Ceci est la ville de Lyon');
INSERT INTO "Ville" ("Id_Ville","Nom","Desc") VALUES (2,'Paris','Ceci est la ville de Paris');
INSERT INTO "dateTips" ("Id_Tips","contenu") VALUES (1,'Arroser régulièrement');
INSERT INTO "dateTips" ("Id_Tips","contenu") VALUES (2,'Exposition au soleil');
COMMIT;
