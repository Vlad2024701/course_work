use master
alter database AutoParking set single_user with rollback immediate
go
drop database AutoParking
go
create database AutoParking
go