drop database if exists dbInfo;

/*Criando o banco de dados */
create database dbInfo
default character set utf8
default collate utf8_general_ci;

/*Usando o banco de dados */
use dbInfo;

/*Usando um usuário para o banco de dados e atribuindo privilégios */
create user 'userLojaInfo'@'localhost' identified with mysql_native_password by '1234567';
grant all privileges on dbInfo.* TO 'userLojaInfo'@'localhost';

/*Criando as tabelas do banco de dados */
create table tbUsuario(
	CodUsuario int primary key auto_increment,
	NomeUsuario varchar(40) not null,
	SenhaUsuario varchar(15) not null,
    Tipo int
);

create table tbFuncionario(
	CodFunc int primary key auto_increment,
	NomeFunc varchar(50) not null,
	TelFunc varchar(20),
    StatusFunc varchar(10)
);

create table tbCliente(
	CodCli int primary key auto_increment,
	NomeCli varchar(50) not null,
	TelCli varchar(50),
	EmailCli varchar(50),
    StatusCli varchar(10)
);

create table tbProduto(
	CodProd int primary key auto_increment,
    NomeProd varchar(160),
    DescProd longtext,
    ValorUnitario decimal(6,2),
    StatusProd varchar(10)
);

create table tbVenda (
	CodVenda int primary key,
    DataVenda date not null,
    Total decimal (10,2),
    FormaPagamento varchar(30),
    StatusVenda varchar(30),
    Obs longtext,
    CodFunc int not null,
    CodCli int not null,
    foreign key (CodFunc) references tbFuncionario (CodFunc),
    foreign key (CodCli) references tbCliente (CodCli)
);

create table tbFormaPagamento(
	CodForma int key auto_increment,
    Descricao varchar(20)
);

create table tbStatusVenda(
	CodStatus int key auto_increment,
    Descricao varchar(20)
);

insert into tbStatusVenda(Descricao)
values('Aguardando Pagamento'),
	  ('Cancelada'),
	  ('Vencido'),
	  ('Em analise'),
	  ('Paga');

create table tbProdutoVenda (
    CodVenda int not null,
    CodProd int not null,
    primary key (CodVenda, CodProd),
    foreign key (CodVenda) references tbVenda (CodVenda),
    foreign key (CodProd) references tbProduto (CodProd),
    Quantidade int not null,
    ValorUnitario decimal(10,2) not null,
    Subtotal decimal(8,2) as (Quantidade*ValorUnitario)
);

/* Criando as procedures */

delimiter $$
create procedure createCliente (
	in vNomeCli varchar(50),
	in vTelCli varchar(50),
	in vEmailCli varchar(50),
    in vStatusCli varchar(10)
)
begin
	insert into tbCliente(NomeCli, TelCli, EmailCli, StatusCli)
	values(vNomeCli, vTelCli, vEmailCli, vStatusCli);
end $$
delimiter ;

delimiter $$
create procedure readCliente ()
begin
	select 
    CodCli as `Codigo`,
    NomeCli as `Nome`,
    TelCli as `Telefone`,
    EmailCli as `Email`,
    StatusCli as `Status`
    from tbCliente
    where StatusCli = 'Ativo' ;
end $$
delimiter ;

delimiter $$
create procedure buscaCliente (in variavel varchar(50))
begin
	select * from tbCliente where 
    CodCli = variavel or 
	NomeCli like concat('%',variavel,'%') or
    TelCli = variavel or
    EmailCli = variavel;
end $$
delimiter ;

call createCliente('Sasha Braus','1198765435','sasha@aot.com','Ativo');
call createCliente('Erwin Smith','1198454435','erwin@aot.com','Ativo');
call createCliente('Connie Springer','1124343345','connie@aot.com','Inativo');

call readCliente();
select * from tbCliente;

call buscaCliente('Erwin');

-- Produtos

delimiter $$
create procedure createProd (
	in vNomeProd varchar(160),
	in vDescProd longtext,
	in vValorUnitario decimal(6,2),
	in vStatusProd varchar(10)
)
begin
	insert into tbProduto(NomeProd, DescProd, ValorUnitario, StatusProd)
	values(vNomeProd, vDescProd, vValorUnitario, vStatusProd);
end $$
delimiter ;

delimiter $$
create procedure readProd ()
begin
	select 
    CodProd as `Codigo`,
    NomeProd as `Nome`,
    ValorUnitario as `Valor Unitario`,
    DescProd as `Descricao`,
    StatusProd as `Status`
    from tbProduto
    where StatusProd = 'Ativo' ;
end $$
delimiter ;

delimiter $$
create procedure buscaProd (in variavel varchar(50))
begin
	select * from tbProduto where 
    CodProd = variavel or 
	NomeProd like concat('%',variavel,'%');
end $$
delimiter ;

delimiter $$
create procedure deleteProd (in variavel int)
begin
	delete from tbProduto where CodProd = variavel;
end $$
delimiter ;

call createProd('Teclado e Mouse','Sem Fio 2.4 Ghz Multimidia Preto USB - Multilaser TC183','220.90','Ativo');
call createProd('HD externo','1tb usb portátil Elements Western Digital CX 1 UN','454.70','Ativo');
call createProd('Processador','AMD RYZEN 9 5900X 12 CORES 3.7GHZ (4.8GHZ TURBO) 70MB CACHE AM4, 100-100000061WOF','3879.12','Ativo');
call createProd('Notebook Asus','Processador Core i3, 4GB de Memória, 256GB SSD de Armazenamento, Tela de 15.6", X543UA-GQ3157T - CX 1 UN','2879.12','Inativo');
call createProd('Notebook Asus','Processador Core i3, 4GB de Memória, 256GB SSD de Armazenamento, Tela de 15.6", X543UA-GQ3157T - CX 1 UN','2879.12','Inativo');

call readCliente();
call readProd();
select * from tbProduto;

call buscaProd('HD');
call deleteProd(5);





delimiter $$
create procedure createFunc (
	in vNomeFunc varchar(50),
	in vTelFunc varchar(50),
    in vStatusFunc varchar(10)
)
begin
	insert into tbFuncionario(NomeFunc, TelFunc, StatusFunc)
	values(vNomeFunc, vTelFunc, vStatusFunc);
end $$
delimiter ;

delimiter $$
create procedure readFunc ()
begin
	select 
    CodFunc as `Codigo`,
    NomeFunc as `Nome`,
    TelFunc as `Telefone`,
    StatusFunc as `Status`
    from tbFuncionario
    where StatusFunc = 'Ativo' ;
end $$
delimiter ;

delimiter $$
create procedure buscaFunc (in variavel varchar(50))
begin
	select * from tbFuncionario where 
    CodFunc = variavel or 
	NomeFunc like concat('%',variavel,'%') or
    TelFunc = variavel;
end $$
delimiter ;

call createFunc('Levi Ackerman', '1154334553', 'Ativo');
call createFunc('Hange Zoe', '1154345665', 'Inativo');
call createFunc('Zeke Yeager', '1154765754', 'Ativo');

call readFunc();
call buscaFunc('Zeke');


delimiter $$
create procedure createVenda (
	in vDataVenda date,
	in vTotal decimal(10,2),
    in vFormaPagamento varchar(30),
    in vStatusVenda varchar(30),
    in vObs longtext,
    in vCodFunc int,
    in vCodCli int
)
begin
	insert into tbVenda(DataVenda, Total, FormaPagamento, StatusVenda, Obs, CodFunc, CodCli)
	values(vDataVenda, vTotal, vFormaPagamento, vStatusVenda, vObs, vCodFunc, vCodCli);
end $$
delimiter ;


delimiter $$
create procedure createProdVenda (
	in vCodVenda int,
	in vCodProd int,
    in vQuantidade int,
    in vValorUnitario decimal(10,2)
)
begin
	insert into tbProdutoVenda(CodVenda, CodProd, Quantidade, ValorUnitario)
	values(vCodVenda, vCodProd, vQuantidade, vValorUnitario);
    update tbVenda set Total = sum(Subtotal) where CodVenda = vCodVenda;
end $$
delimiter ;

call createVenda('2021-04-05', 0, 'Cartão', 'Aguardando Pagamento', 'Duas unidades de teclado e mouse', 1,2);
call createVenda('2021-04-05', 0, 'Dinheiro', 'Aguardando Pagamento', 'HD', 2,3);
call createProdVenda(1, 1, 2, 220.90);
call createProdVenda(2, 2, 1, 454.70);

  
  
delimiter $$
create procedure TestarUsuario( 
	in vNomeUsuario varchar(40),
	in vSenhaUsuario varchar(15)
)
begin
	select * from `tbUsuario` where `NomeUsuario` = vNomeUsuario and `SenhaUsuario` = vSenhaUsuario;
end $$
delimiter ;

select * from tbFormaPagamento;
select * from tbProdutoVenda;
insert into tbFormaPagamento(descricao) values('Dinheiro'), ('Cartão');


delimiter $$
create procedure deleteCliente (in variavel int)
begin
	delete from tbCliente where CodCli = variavel;
end $$
delimiter ;

insert into tbUsuario (NomeUsuario, SenhaUsuario, Tipo)
values ('Mikasa','54321',1),
('Levi','12345',2);
select * from tbformaPagamento;

select * from viewVenda;



drop table tbProdutoVenda;
drop table tbVenda;

create table tbVenda(
	CodVenda int primary key,
    DataVenda date,
    CodFunc int,
    CodCli int,
    CodStatus int,
    CodForma int,
    Arquivar boolean,
    foreign key (CodFunc) references tbFuncionario (CodFunc),
    foreign key (CodCli) references tbCliente (CodCli),
    foreign key (CodStatus) references tbStatusVenda (CodStatus),
    foreign key (CodForma) references tbFormaPagamento (CodForma)
);

create table tbItens(
    CodVenda int not null,
    CodProd int not null,
    primary key (CodVenda, CodProd),
    foreign key (CodVenda) references tbVenda (CodVenda),
    foreign key (CodProd) references tbProduto (CodProd),
    Quantidade int not null,
    ValorUnitario decimal(10,2) not null,
    Subtotal decimal(8,2) as (Quantidade*ValorUnitario)
);

delete from tbItens where CodVenda = 0 and CodProd = 0;


drop view viewVenda;
create view viewVenda
as select
	tbVenda.CodVenda,
	tbItens.CodProd,
    DATE_FORMAT(tbVenda.DataVenda, "%d/%m/%Y") as 'DataVenda',
	tbFormaPagamento.Descricao as 'FormaPagamento',
	tbStatusVenda.Descricao as 'StatusVenda',
    tbFuncionario.NomeFunc,
    tbCliente.NomeCli,
    tbProduto.NomeProd,
    tbItens.Quantidade,
    tbItens.ValorUnitario,
    tbItens.Subtotal,
    tbVenda.Arquivar
from tbVenda 
    inner join tbCliente on tbVenda.CodCli = tbCliente.CodCli
	inner join tbFuncionario on tbVenda.CodFunc = tbFuncionario.CodFunc
    inner join tbFormaPagamento on tbVenda.CodForma = tbFormaPagamento.CodForma
    inner join tbStatusVenda on tbVenda.CodStatus = tbStatusVenda.CodStatus
    inner join tbItens on tbVenda.CodVenda = tbItens.CodVenda
    inner join tbProduto on tbProduto.CodProd = tbItens.CodProd;
    

select 
	sum(Subtotal) as 'Total' ,
	sum(Quantidade) as 'Quantidade'
from tbItens where CodVenda = 0;

update tbVenda set Arquivar = 1 where CodVenda = 0;

select * from viewVenda;

delete from tbVenda where CodVenda = 1;

select CodVenda, DataVenda, NomeFunc, NomeCli, StatusVenda, sum(Subtotal) as 'Total' from viewVenda;


select CodVenda, DataVenda, NomeFunc, NomeCli, StatusVenda, sum(Subtotal) as 'Total' from viewVenda;


select sum(Subtotal) as 'Total' from viewVenda where CodVenda = 1;

select * from viewVenda where CodVenda = 1;


select * from tbvenda;
select * from viewVenda;


select * from tbCliente where NomeCli like '%@pesquisa%' or CodCli like '%@pesquisa%';


delimiter $$
create procedure PesquisarCliente(
	in pesquisa varchar(60)
)
begin
	select * from viewCliente
	where `Nome` like concat('%',pesquisa,'%') or
	      `Código` like concat('%',pesquisa,'%');
end $$
delimiter ;

delimiter $$
create procedure PesquisarProduto(
	in pesquisa varchar(60)
)
begin
	select * from viewProduto
	where `Produto` like concat('%',pesquisa,'%') or
	      `Código` like concat('%',pesquisa,'%');
end $$
delimiter ;

delimiter $$
create procedure PesquisarFuncionario(
	in pesquisa varchar(60)
)
begin
	select * from viewFuncionario
	where `Nome` like concat('%',pesquisa,'%') or
	      `Código` like concat('%',pesquisa,'%');
end $$
delimiter ;

call PesquisarCliente('Sa');


create view viewCliente
as select
	tbCliente.CodCli as 'Código',
	tbCliente.NomeCli as 'Nome',
	tbCliente.TelCli as 'Telefone',
	tbCliente.EmailCli as 'Email',
	tbCliente.StatusCli as 'Status'
from tbCliente;

create view viewFuncionario
as select
	tbFuncionario.CodFunc as 'Código',
    tbFuncionario.NomeFunc as 'Nome',
    tbFuncionario.TelFunc as 'Telefone',
    tbFuncionario.StatusFunc as 'Status'
from tbFuncionario;

create view viewProduto
as select
	tbProduto.CodProd as 'Código',
	tbProduto.NomeProd as 'Produto',
	tbProduto.DescProd as 'Descrição',
	tbProduto.ValorUnitario as 'Valor unitário',
	tbProduto.StatusProd as 'Status'
from tbProduto;

select * from tbUsuario;