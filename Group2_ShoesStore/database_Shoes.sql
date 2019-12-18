drop database if exists ShoesStore;
create database if not exists ShoesStore;
use ShoesStore;
create table if not exists Customers(
customer_id int primary key auto_increment,
customer_account varchar(50) not null,
customer_password varchar(50) not null,
customer_email varchar(50) not null,
customer_name varchar(50),
customer_phone varchar(15),
customer_birthday date ,
customer_gender varchar(10) ,
customer_address varchar(100) 
);
insert into Customers(customer_account,customer_password,customer_name, customer_email)
value 
('xuan','pinkass','le ngoc xuan','lexuanxdts@gmail.com'),
('tuananh','123456','le ngoc x','lexuan281294@gmail.com'),
('ngoc','123456','le ngoc','xuanln.nde19011@vtc.edu.vn');

create table if not exists Orders(
order_id int  primary key auto_increment,
order_customer int not null,
order_date datetime,
order_status int,
constraint fk_Orders_Customers foreign key(order_customer) references Customers(customer_id)
);


create table if not exists Items(
item_id int primary key auto_increment,
item_name varchar(100) not null,
item_price float not null,
item_quantity int not null,
item_size int not null,
item_color varchar(20),
item_material nvarchar(50),
item_trademark nvarchar(50)
);
insert into Items(item_id,item_name,item_price,item_quantity,item_size,item_color,item_material,item_trademark)
value
('1','Loafer','250000','5','42','black','fabric','vans'),
('2','Jelli','350000','5','39','red','fabric','vans'),
('3','Juzi','340000','5','40','red','fabric','vans'),
('4','Baby','400000','5','41','red','fabric','vans'),
('5','Boobby','500000','5','42','white','fabric','vans'),
('6','Flat','450000','5','43','black','fabric','vans'),
('7','Flus','300000','5','39','black','fabric','adidas'),
('8','Oxfot','320000','5','40','black','fabric','adidas'),
('9','Effen','1000000','5','41','red','fabric','adidas'),
('10','Dock','700000','5','42','red','fabric','adidas'),
('11','Duck','600000','5','43','yeallow','fabric','adidas'),
 ('12','Mary','530000','5','41','yeallow','fabric','adidas'),
('13','Jane','250000','5','39','black','fabric','gucci'),
('14','Moca','250000','5','39','red','fabric','gucci'),
('15','Mocha','250000','5','39','gold','fabric','gucci'),
('16','Kitten','400000','5','40','black','fabric','gucci'),
('17','Cap','400000','5','40','red','fabric','gucci'),
('18','Toe','400000','5','40','gold','fabric','gucci'),
('19','Point','400000','5','40','pink','fabric','gucci'),
('20','Open','450000','5','41','white','fabric','gucci'),
('21','Pla','5000000','5','39','black','fabric','balenciaga'),
('22','Still','5000000','5','39','white','fabric','balenciaga'),
('23','Ank','5000000','5','39','red','fabric','balenciaga'),
('24','Strap','7000000','5','40','black','fabric','balenciaga'),
('25','Stip','7000000','5','41','pink','fabric','balenciaga'),
('26','Chip','7000000','5','42','gold','fabric','balenciaga'),
('27','Angel','8000000','5','43','black','fabric','balenciaga'),
('28','Lemon','2000000','5','39','black','fabric','nike'),
('29','Bupp','2000000','5','39','gold','fabric','nike'),
('30','Glo','2000000','5','39','white','fabric','nike'),
('31','Neo','2000000','5','39','pink','fabric','nike'),
('32','Leo','2200000','5','40','black','fabric','nike'),
('33','Neol','2200000','5','40','gold','fabric','nike'),
('34','Ragging','2200000','5','40','pink','fabric','nike'),
('35','Buff','3000000','5','41','black','fabric','nike'),
('36','Topp','2500000','5','39','black','fabric','puma'),
('37','Noel','2500000','5','39','white','fabric','puma'),
('38','Pap','2500000','5','39','gold','fabric','puma'),
('39','Pew','2500000','5','39','pink','fabric','puma'),
('40','Mix','3000000','5','40','red','fabric','puma'),
('41','Gaming','3000000','5','40','black','fabric','puma'),
('42','Wed','3000000','5','40','gold','fabric','puma'),
('43','Chun','3000000','5','40','pink','fabric','puma'),
('44','Sling','3500000','5','41','black','fabric','puma'),
('45','Gladiator','3500000','5','41','red','fabric','puma'),
('46','Sandal','3500000','5','41','gold','fabric','puma'),
('47','Cold','5250000','5','42','white','fabric','vans'),
('48','Snow','650000','5','42','white','fabric','vans'),
('49','Ugg','6250000','5','42','gold','fabric','adidas'),
('50','Lita','4250000','5','42','black','fabric','gucci');

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
select ord.order_id, ord.order_date, it.item_name from orders ord inner join orderDetails ordt on ord.order_id = ordt.order_id inner join Items it on ordt.item_id = it.item_id where ord.order_customer = 1 and ord.order_status = 1;



