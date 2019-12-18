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
insert into Items(item_name,item_price,item_quantity,item_size,item_color,item_material,item_trademark)
value
('Loafer','250000','5','42','black','fabric','vans'),
('Jelli','350000','5','39','red','fabric','vans'),
('Juzi','340000','5','40','red','fabric','vans'),
('Baby','400000','5','41','red','fabric','vans'),
('Boobby','500000','5','42','white','fabric','vans'),
('Flat','450000','5','43','black','fabric','vans'),
('Flus','300000','5','39','black','fabric','adidas'),
('Oxfot','320000','5','40','black','fabric','adidas'),
('Effen','1000000','5','41','red','fabric','adidas'),
('Dock','700000','5','42','red','fabric','adidas'),
('Duck','600000','5','43','yeallow','fabric','adidas'),
 ('Mary','530000','5','41','yeallow','fabric','adidas'),
('Jane','250000','5','39','black','fabric','gucci'),
('Moca','250000','5','39','red','fabric','gucci'),
('Mocha','250000','5','39','gold','fabric','gucci'),
('Kitten','400000','5','40','black','fabric','gucci'),
('Cap','400000','5','40','red','fabric','gucci'),
('Toe','400000','5','40','gold','fabric','gucci'),
('Point','400000','5','40','pink','fabric','gucci'),
('Open','450000','5','41','white','fabric','gucci'),
('Pla','5000000','5','39','black','fabric','balenciaga'),
('Still','5000000','5','39','white','fabric','balenciaga'),
('Ank','5000000','5','39','red','fabric','balenciaga'),
('Strap','7000000','5','40','black','fabric','balenciaga'),
('Stip','7000000','5','41','pink','fabric','balenciaga'),
('Chip','7000000','5','42','gold','fabric','balenciaga'),
('Angel','8000000','5','43','black','fabric','balenciaga'),
('Lemon','2000000','5','39','black','fabric','nike'),
('Bupp','2000000','5','39','gold','fabric','nike'),
('Glo','2000000','5','39','white','fabric','nike'),
('Neo','2000000','5','39','pink','fabric','nike'),
('Leo','2200000','5','40','black','fabric','nike'),
('Neol','2200000','5','40','gold','fabric','nike'),
('Ragging','2200000','5','40','pink','fabric','nike'),
('Buff','3000000','5','41','black','fabric','nike'),
('Topp','2500000','5','39','black','fabric','puma'),
('Noel','2500000','5','39','white','fabric','puma'),
('Pap','2500000','5','39','gold','fabric','puma'),
('Pew','2500000','5','39','pink','fabric','puma'),
('Mix','3000000','5','40','red','fabric','puma'),
('Gaming','3000000','5','40','black','fabric','puma'),
('Wed','3000000','5','40','gold','fabric','puma'),
('Chun','3000000','5','40','pink','fabric','puma'),
('Sling','3500000','5','41','black','fabric','puma'),
('Gladiator','3500000','5','41','red','fabric','puma'),
('Sandal','3500000','5','41','gold','fabric','puma'),
('Cold','5250000','5','42','white','fabric','vans'),
('Snow','650000','5','42','white','fabric','vans'),
('Ugg','6250000','5','42','gold','fabric','adidas'),
('Lita','4250000','5','42','black','fabric','gucci');


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


