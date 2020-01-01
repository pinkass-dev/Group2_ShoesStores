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
customer_birthday date,
customer_gender varchar(10) ,
customer_address varchar(100),
UserShoppingCart bool default false,
constraint uq_Customers_email unique(customer_email)
);

insert into Customers(customer_account,customer_password,customer_name, customer_email,customer_phone,customer_birthday,customer_gender,customer_address)
value 
('xuan','123456','le ngoc xuan','lexuanxdts@gmail.com','0987541656','1994-12-28','male','ha noi');
create table if not exists Orders(
order_id int(10)  primary key auto_increment,
order_customer int not null,
order_date datetime,
order_status int,
constraint fk_Orders_Customers foreign key(order_customer) references Customers(customer_id)
);


create table if not exists Items(
item_id int primary key ,
item_name varchar(50) not null,
item_price float not null,
item_color varchar(20),
item_material varchar(20),
item_trademark nvarchar(20)
);
insert into Items(item_id,item_name,item_price,item_color,item_material,item_trademark)
value
('1','Loafer','250000','black','fabric','vans'),
('2','Jelli','350000','red','fabric','vans'),
('3','Juzi','340000','red','fabric','vans'),
('4','Baby','400000','red','fabric','vans'),
('5','Boobby','500000','white','fabric','vans'),
('6','Flat','450000','black','fabric','vans'),
('7','Flus','300000','black','fabric','adidas'),
('8','Oxfot','320000','black','fabric','adidas'),
('9','Effen','1000000','red','fabric','adidas'),
('10','Dock','700000','red','fabric','adidas'),
('11','Duck','600000','yeallow','fabric','adidas'),
 ('12','Mary','530000','yeallow','fabric','adidas'),
('13','Jane','250000','black','fabric','gucci'),
('14','Moca','250000','red','fabric','gucci'),
('15','Mocha','250000','gold','fabric','gucci'),
('16','Kitten','400000','black','fabric','gucci'),
('17','Cap','400000','red','fabric','gucci'),
('18','Toe','400000','gold','fabric','gucci'),
('19','Point','400000','pink','fabric','gucci'),
('20','Open','450000','white','fabric','gucci');


select * from Items;
create table  if not exists ItemDetails(
item_id int primary key,
item_size int not null,
item_quantity int not null,
constraint fk_itemdetails_items foreign key (item_id) references Items(item_id)
);

insert into ItemDetails (item_id,item_size,item_quantity)
value
('1','39','5'),
('2','40','5'),
('3','40','5'),
('4','40','5'),
('5','40','5'),
('6','41','5'),
('7','41','5'),
('8','41','5'),
('9','41','5'),
('10','39','5'),
('11','39','5'),
('12','39','5'),
('13','39','5'),
('14','30','5'),
('15','40','5'),
('16','40','5'),
('17','41','5'),
('18','42','5'),
('19','42','5'),
('20','39','5');




select order_id from orders where order_customer = 2 order by order_id  desc limit 1 ;
create table if not exists Orderdetails(
order_id int(10) not null,
item_id int not null,
constraint primary key(order_id, item_id),
constraint fk_Orderdetails_Orders foreign key(order_id) references Orders(order_id),
constraint fk_Orderdetails_Items foreign key (item_id) references Items(item_id)
);
select order_id from orders where order_customer = 1 order by order_id desc limit 1;
select * from orders;
select * from customers;
select * from orderdetails;
select * from orders where order_customer = 1 order by order_id desc limit 1;
select * from Orderdetails where item_id = 1;

select order_status from Orders ord inner join OrderDetails ordl on ord.order_id = ordl.order_id where order_customer = 1;
select ord.order_id, ord.order_date, it.item_name from orders ord inner join orderDetails ordt on ord.order_id = ordt.order_id inner join Items it on ordt.item_id = it.item_id where ord.order_customer = 1 and ord.order_status = 1 limit 1;
-- insert into orders (orderUser,orderStatus) values(1,0);
-- insert into orderdetails(order_id,item_id) values (1,4);

select order_id from orders order by order_id desc limit 1;
use ShoesStore;

select it.item_id,it.item_name,it.item_price,it.item_color,it.item_material,it.item_trademark,itd.item_size,itd.item_quantity from items it, itemdetails itd where it.item_id = itd.item_id;
create user if not exists 'root'@'localhost' identified by 'Lnx846061';
grant all on customers to 'root'@'localhost';
grant all on items to 'root'@'localhost';
grant all on orders to 'root'@'localhost';
grant all on orderdetails to 'root'@'localhost';
grant all on ratings to 'root'@'localhost';
grant lock tables on ShoesStore.* to 'root'@'localhost';

