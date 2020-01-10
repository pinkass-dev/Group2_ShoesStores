drop database if exists ShoesStore;
create database if not exists ShoesStore;
use ShoesStore;
create table if not exists Customers(
customer_id int primary key auto_increment,
customer_account varchar(50) not null unique,
customer_password varchar(50) not null,
customer_email varchar(50) not null unique,
customer_name nvarchar(50),
customer_phone varchar(15),
customer_birthday varchar(20),
customer_gender varchar(10) ,
customer_address varchar(100),
customer_balance decimal(10.2),
UserShoppingCart bool default false,
constraint uq_Customers_email unique(customer_email)
);
insert into Customers(customer_account,customer_password,customer_name, customer_email,customer_phone,customer_birthday,customer_gender,customer_address,customer_balance)
value 
('xuan','123456','le ngoc xuan','lexuanxdts@gmail.com','0987541656','1994-12-28','male','ha noi','10000000');
create table if not exists Orders(
order_id int(10)  primary key auto_increment,
order_customer int not null,
order_date datetime,
order_status int,
constraint fk_Orders_Customers foreign key(order_customer) references Customers(customer_id)
);
select * from Customers;
create table if not exists Items(
item_id int primary key ,
item_name nvarchar(100) not null,
item_price float not null,
item_color varchar(20),
item_material varchar(20),
item_trademark nvarchar(20)
);
insert into Items(item_id,item_name,item_price,item_color,item_material,item_trademark)
value
('1','loafer','250000','black','fabric','vans'),
('2','jelli','350000','red','fabric','vans'),
('3','juzi','340000','red','fabric','vans'),
('4','baby','400000','red','fabric','vans'),
('5','boobby','500000','white','fabric','vans'),
('6','Flat','450000','black','fabric','vans'),
('7','flus','300000','black','fabric','adidas'),
('8','oxfot','320000','black','fabric','adidas'),
('9','effen','1000000','red','fabric','adidas'),
('10','dock','700000','red','fabric','adidas'),
('11','duck','600000','yeallow','fabric','adidas'),
('12','mary','530000','yeallow','fabric','adidas'),
('13','jane','250000','black','fabric','gucci'),
('14','moca','250000','red','fabric','gucci'),
('15','mocha','250000','gold','fabric','gucci'),
('16','kitten','400000','black','fabric','gucci'),
('17','cap','400000','red','fabric','gucci'),
('18','toe','400000','gold','fabric','gucci'),
('19','point','400000','pink','fabric','gucci'),
('20','open','450000','white','fabric','gucci'),
('21','Pla','5000000','black','fabric','balenciaga'),
('22','Still','5000000','white','fabric','balenciaga'),
('23','Ank','5000000','red','fabric','balenciaga'),
('24','Strap','7000000','black','fabric','balenciaga'),
('25','Stip','7000000','pink','fabric','balenciaga'),
('26','Chip','7000000','gold','fabric','balenciaga'),
('27','Angel','8000000','black','fabric','balenciaga'),
('28','Lemon','2000000','black','fabric','nike'),
('29','Bupp','2000000','gold','fabric','nike'),
('30','Glo','2000000','white','fabric','nike'),
('31','Neo','2000000','pink','fabric','nike'),
('32','Leo','2200000','black','fabric','nike'),
('33','Neol','2200000','gold','fabric','nike'),
('34','Ragging','2200000','pink','fabric','nike'),
('35','Buff','3000000','black','fabric','nike'),
('36','Topp','2500000','black','fabric','puma'),
('37','Noel','2500000','white','fabric','puma'),
('38','Pap','2500000','gold','fabric','puma'),
('39','Pew','2500000','pink','fabric','puma'),
('40','Mix','3000000','red','fabric','puma'),
('41','Gaming','3000000','black','fabric','puma'),
('42','Wed','3000000','gold','fabric','puma'),
('43','Chun','3000000','pink','fabric','puma'),
('44','Sling','3500000','black','fabric','puma'),
('45','Gladiator','3500000','red','fabric','puma'),
('46','Sandal','3500000','gold','fabric','puma'),
('47','Cold','5250000','white','fabric','vans'),
('48','Snow','650000','white','fabric','vans'),
('49','Ugg','6250000','gold','fabric','adidas'),
('50','Lita','4250000','black','fabric','gucci'),
('51','Adam1234','250000','black','fabric','dolce'),
('52','Adam113','250000','red','fabric','dolce'),
('53','Adam121','250000','gold','fabric','dolce'),
('54','Adam789','250000','white','fabric','dolce'),
('55','Adam777','250000','pink','fabric','dolce'),
('56','Adam666','250000','gold','fabric','dolce'),
('57','Adam111','250000','white','fabric','dolce'),
('58','Adam222','250000','green','fabric','dolce'),
('59','Adam333','300000','black','fabric','dolce'),
('60','Mod111','250000','black','fabric','chanel'),
('61','Mod222','250000','white','fabric','chanel'),
('62','Mod333','250000','whiteblack','fabric','chanel'),
('63','Mod444','250000','gold','fabric','chanel'),
('64','Mod555','250000','black','fabric','chanel'),
('65','Mod666','400000','white','fabric','chanel'),
('66','Mod777','456000','gold','fabric','chanel'),
('67','Mod888','250000','black','fabric','chanel'),
('68','Mod999','500000','pink','fabric','chanel'),
('69','Mod789','1000000','black','fabric','chanel'),
('70','Madison1','2222222','black','fabric','hermes'),
('71','Madson22','5000000','brown','fabric','hermes'),
('72','Madiso33','5000000','white','fabric','hermes'),
('73','Madiso44','4500000','gold','fabric','hermes'),
('74','Madiso55','345000','black','fabric','hermes'),
('75','Madison66','250000','orange','fabric','hermes');




select * from Customers;

select * from Items;
create table  if not exists ItemDetails(
item_id int primary key,
item_size nvarchar(20) not null,
item_quantity int not null,
constraint fk_itemdetails_items foreign key (item_id) references Items(item_id)
);

insert into ItemDetails (item_id,item_size,item_quantity)
value
('1','39,40','5'),
('2','39,40','5'),
('3','39,40','5'),
('4','39,40','5'),
('5','39,40','5'),
('6','39,40','5'),
('7','39,40','5'),
('8','39,40','5'),
('9','39,40','5'),
('10','39,40','5'),
('11','39,40','5'),
('12','39,40','5'),
('13','39,40','5'),
('14','39,40','5'),
('15','39,40','5'),
('16','39,40','5'),
('17','39,40','5'),
('18','39,40','5'),
('19','39,40','5'),
('20','39,40','5'),
('21','39,40','5'),
('22','39,40','5'),
('23','39,40','5'),
('24','39,40','5'),
('25','39,40','5'),
('26','39,40','5'),
('27','39,40','5'),
('28','39,40','5'),
('29','39,40','5'),
('30','39,40','5'),
('31','39,40','5'),
('32','39,40','5'),
('33','39,40','5'),
('34','39,40','5'),
('35','39,40','5'),
('36','39,40','5'),
('37','39,40','5'),
('38','39,40','5'),
('39','39,40','5'),
('40','39,40','5'),
('41','39,40','5'),
('42','39,40','5'),
('43','39,40','5'),
('44','39,40','5'),
('45','39,40','5'),
('46','39,40','5'),
('47','39,40','5'),
('48','39,40','5'),
('49','39,40','5'),
('50','39,40','5'),
('51','39,40','5'),
('52','39,40','5'),
('53','39,40','5'),
('54','39,40','5'),
('55','39,40','5'),
('56','39,40','5'),
('57','39,40','5'),
('58','39,40','5'),
('59','39,40','5'),
('60','39,40','5'),
('61','39,40','5'),
('62','39,40','5'),
('63','39,40','5'),
('64','39,40','5'),
('65','39,40','5'),
('66','39,40','5'),
('67','39,40','5'),
('68','39,40','5'),
('69','39,40','5'),
('70','39,40','5'),
('71','39,40','5'),
('72','39,40','5'),
('73','39,40','5'),
('74','39,40','5'),
('75','39,40','5');

select item_quantity from itemdetails where item_id = 2;

select order_id from orders where order_customer = 2 order by order_id  desc limit 1 ;
create table if not exists Orderdetails(
order_id int(10) not null,
item_id int not null,
amount int,
constraint primary key(order_id),
constraint fk_Orderdetails_Orders foreign key(order_id) references Orders(order_id),
constraint fk_Orderdetails_Items foreign key (item_id) references Items(item_id)
);
select order_id from orders where order_customer = 1 order by order_id desc limit 1;
select * from orders;
select * from customers;
select * from orderdetails;--
select * from orders where order_customer = 1 order by order_id desc limit 1;
select * from Orderdetails where item_id = 1;

select order_status from Orders ord inner join OrderDetails ordt on ord.order_id = ordt.order_id where order_customer = 1;
select ord.order_id, ord.order_date, it.item_name from orders ord inner join orderDetails ordt on ord.order_id = ordt.order_id inner join Items it on ordt.item_id = it.item_id where ord.order_customer = 1 and ord.order_status = 1 limit 1;
-- insert into orders (order_customer,order_status) values(1,0);
-- insert into orderdetails(order_id,item_id) values (1,4);


select it.item_id,it.item_name,itd.item_size,it.item_price,it.item_color,it.item_material,it.item_trademark, itd.item_quantity from items it, itemdetails itd where it.item_id = itd.item_id limit 10;
select order_id from orders order by order_id desc limit 1;
use ShoesStore;


create user if not exists 'root'@'localhost' identified by 'Lnx846061';
grant all on customers to 'root'@'localhost';
grant all on items to 'root'@'localhost';
grant all on orders to 'root'@'localhost';
grant all on orderdetails to 'root'@'localhost';
grant all on ratings to 'root'@'localhost';
grant lock tables on ShoesStore.* to 'root'@'localhost';

