CREATE TABLE Usuarios (
   cpf_user VARCHAR(11) NOT NULL,
   name_user varchar(100) NOT NULL,
   login_user varchar(50) NOT NULL,
   password_user VARCHAR (50) NOT NULL,
   datebirth_user DATE NOT NULL,
   adm_user BIT NOT NULL,
   PRIMARY KEY (cpf_user)
);

CREATE TABLE Posts (
   id_post INT,
   title_post varchar(100) NOT NULL,
   description_post TEXT NOT NULL,
   image_post VARCHAR (MAX) NOT NULL,
   date_post DATETIME2 NOT NULL,
   likes_post INT NOT NULL,
   cpf_user INT NOT NULL,
   PRIMARY KEY (id_post)
);


ALTER TABLE Posts ADD CONSTRAINT FK_User FOREIGN KEY (cpf_user) REFERENCES Usuarios(cpf_user);
