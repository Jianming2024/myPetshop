drop schema if exists petshop cascade;
create schema if not exists petshop;
create table petshop.seller (
                                id text primary key not null,
                                name text not null
);
create table petshop.pet (
                             id text primary key not null,
                             name text not null,
                             breed text not null,
                             createdAt timestamp with time zone not null,
                             sold_date date default null,
                             price numeric not null,
                             seller text not null references petshop.seller(id)
);