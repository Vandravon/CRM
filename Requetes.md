CREATE DATABASE CRM;

CREATE TABLE users
(
    id int PRIMARY KEY AUTO_INCREMENT, 
    email varchar(255),
    password varchar(255),
    firstName varchar(255),
    lastName varchar(255),
    confirmedPassword varchar(255),
    grants varchar(50)
);

CREATE TABLE clients
(
    id int PRIMARY KEY AUTO_INCREMENT,
    name varchar(255),
    state varchar(50),
    tva double,
    totalCAHt int,
    comment text,
    user_id int,
    foreign key (user_id) references users(id)
);

CREATE TABLE orders
(
    id int PRIMARY KEY AUTO_INCREMENT,
    typePresta varchar(255),
    client varchar(255),
    nbJours int,
    tjmHt double,
    tva double,
    state varchar(50),
    comment text,
    client_id int,
    foreign key (client_id) references clients(id)
);