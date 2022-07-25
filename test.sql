CREATE Table Customers (
  id int PRIMARY KEY IDENTITY(1,1) not null,
  Name text
  );
  
  
  INSERT INTO Customers VALUES('Max');
  INSERT INTO Customers VALUES('Pavel');
  INSERT INTO Customers VALUES('Ivan');
  INSERT INTO Customers VALUES('Leonid');
  
  CREATE TABLE Orders(
  id INT PRIMARY KEY IDENTITY(1,1) not null,
  CustomerId INT,
  CONSTRAINT FK_Orders_To_Customers FOREIGN KEY (CustomerId) REFERENCES Customers (Id));
  
  INSERT into Orders VALUES(2);
  INSERT into Orders VALUES(4);
  
  //Первый вариант
SELECT Customers.name
  from Customers
  where exists(SELECT * from Orders where Orders.CustomerId=Customers.id)
 
 //Второй вариант
 SELECT Customers.name
FROM Customers
left JOIN Orders on Customers.id=Orders.customerid
WHERE Orders.CustomerId IS NOT NULL 