
DROP PROCEDURE IF EXISTS spInsertCustomer;
Delimiter $$
CREATE PROCEDURE spInsertCustomer(varCustCPF varchar(14), varCustName varchar(150), varCustDtNasc datetime,
varCustGender char(1), varCustTel varchar(14), varCustEmail varchar(150), varCustPassword varchar(18), 
varCustNumberAddress integer, varCEP varchar(10))
BEGIN

IF NOT EXISTS (SELECT CEP FROM tbCep WHERE CEP = varCEP) THEN
            INSERT INTO tbCep (CEP) VALUES (varCEP);
END IF;

IF NOT EXISTS (SELECT CustCPF FROM tbCustomer WHERE CustCPF = varCustCPF) THEN
				INSERT INTO tbCustomer (CustCPF, CustName, CustDtNasc, CustGender, CustTel, CustEmail, CustPassword, CustNumberAddress, CEPAddress)
                VALUES (varCustCPF, varCustName, varCustDtNasc, varCustGender, varCustTel, varCustEmail, varCustPassword, varCustNumberAddress, (SELECT CEP FROM tbCep WHERE CEP = varCEP));
END IF;

END; 
$$ DELIMITER ;


DROP PROCEDURE IF EXISTS spUpdateCustomer;
Delimiter $$
CREATE PROCEDURE spUpdateCustomer(varCustEmail varchar(150), varCustName varchar(100), varCustDtNasc datetime,
varCustGender char(1), varCustPassword varchar(18), 
varCustNumberAddress integer, varCEPAddress varchar(10))
BEGIN

IF NOT EXISTS (SELECT CEP FROM tbCep WHERE CEP = varCEPAddress) THEN
            INSERT INTO tbCep (CEP) VALUES (varCEPAddress);
END IF;
			Update tbCustomer
                SET CustName = varCustName, CustDtNasc = varCustDtNasc, CustGender = varCustGender, CustPassword = varCustPassword, CustNumberAddress = varCustNumberAddress, CEPAddress = varCEPAddress
                WHERE CustEmail = varCustEmail;
END; 
$$ DELIMITER ;

delimiter $$
DROP FUNCTION IF EXISTS F_Validation_Cad;
CREATE FUNCTION F_Validation_Cad(v_cpf varchar(14),v_email VARCHAR(300))
RETURNS VARCHAR(100)
BEGIN

DECLARE  MSG VARCHAR(100) DEFAULT "";
DECLARE  EMAIL int DEFAULT 0;
DECLARE  CPF int DEFAULT 0;
  
  SELECT count(CUSTCPF) into cpf FROM tbCustomer where custcpf = v_cpf;
  SELECT count(CUSTEMAIL) into email FROM tbCustomer where CUSTEMAIL = v_email;
  
  if email > 0 and cpf > 0 then
  set msg = "Email e CPF já utilizados";
  elseif email > 0 then
  set msg = "Email já utilizado";
  elseif cpf > 0 then
  set msg = "CPF já utlizado";
  else
  set msg = "Not Found CPF and Email";
  end if;
 
  RETURN (MSG);
  
  end$$

DELIMITER $$
DROP FUNCTION IF EXISTS F_Validation_Log ;
CREATE FUNCTION F_Validation_Log(v_email VARCHAR(300), v_password varchar(50))
RETURNS VARCHAR(100)
DETERMINISTIC
BEGIN 
  DECLARE  MSG VARCHAR(100) DEFAULT "";
DECLARE  EMAIL int DEFAULT 0;
DECLARE  VPASSWORD int DEFAULT 0;
  
  SELECT count(CUSTEMAIL) into email FROM tbCustomer where CUSTEMAIL = v_email;
  SELECT count(CUSTEMAIL) into vpassword FROM tbCustomer where CUSTEMAIL = v_email AND CUSTPASSWORD = v_password;

  
  if email > 0 and vpassword > 0 then
  set msg = "Login OK";
  elseif email > 0 then
  set msg = "Email OK";
  else
  set msg = "Not Found Email and Password";
  end if;
 
  RETURN MSG;
  
END$$
DELIMITER ;


DROP PROCEDURE IF EXISTS spLoginUser;
DELIMITER $$
CREATE PROCEDURE spLoginUser(varEmailUser varchar(150), varPasswordUser varchar(18))
BEGIN
SELECT * FROM tbCustomer WHERE CustEmail = varEmailUser AND CustPassword = varPasswordUser AND IsDeleted = 'No';
END
$$ DELIMITER ;

DROP PROCEDURE IF EXISTS spSelectEmail;
DELIMITER $$
CREATE PROCEDURE spSelectEmail(varEmailUser varchar(150))
BEGIN
SELECT CustEmail FROM tbCustomer WHERE CustEmail = varEmailUser;
END
$$ DELIMITER ;




DROP PROCEDURE IF EXISTS spInsertEmployee;

Delimiter $$
CREATE PROCEDURE spInsertEmployee(varEmpCPF varchar(14), varEmpName varchar(150), varEmpLogin varchar(50), varEmpDtNasc datetime,
varEmpGender char(1), varEmpTel varchar(14), varEmpEmail varchar(150), varEmpPassword varchar(18), 
varEmpNumberAddress integer, varCEP varchar(10), varRoleName varchar(50))
BEGIN

IF NOT EXISTS (SELECT CEP FROM tbCep WHERE CEP = varCEP) THEN
            INSERT INTO tbCep (CEP) VALUES (varCEP);
END IF;


IF NOT EXISTS (SELECT EmpLogin FROM tbEmployee WHERE EmpLogin = varEmpLogin) THEN

				IF NOT EXISTS (SELECT EmpCPF FROM tbEmployee WHERE EmpCPF = varEmpCPF) THEN
                
								IF NOT EXISTS (SELECT EmpTel FROM tbEmployee WHERE EmpTel = varEmpTel) THEN
											IF NOT EXISTS (SELECT EmpEmail FROM tbEmployee WHERE EmpEmail = varEmpEmail) THEN
																IF NOT EXISTS (SELECT RoleCode FROM tbrole WHERE RoleName = varRoleName) THEN 
																		INSERT INTO tbRole (RoleName) VALUES (varRoleName);
																END IF; -- End RoleCode
                                
														INSERT INTO tbEmployee (EmpCPF, EmpName, EmpLogin, EmpDtNasc, EmpGender, EmpTel, EmpEmail, EmpPassword, EmpNumberAddress, EmpRole, CEPAddress)
														VALUES (varEmpCPF, varEmpName, varEmpLogin, varEmpDtNasc, varEmpGender, varEmpTel, varEmpEmail, varEmpPassword, varEmpNumberAddress, (SELECT RoleCode FROM tbrole WHERE RoleName = varRoleName), (SELECT CEP FROM tbCep WHERE CEP = varCEP));
                                            END IF; -- End EmpEmail
								END IF; -- End EmpTel
                END IF; -- End EmpCPF
END IF; -- End EmpLogin

END; 
$$ DELIMITER ;

DROP PROCEDURE IF EXISTS spInsertProduct;

DELIMITER $$
CREATE PROCEDURE spInsertProduct (varProdBarCode int, varProdName varchar(100), varProdPrice decimal(8,2), varCatName varchar(100))
BEGIN
IF NOT EXISTS (SELECT ProdBarCode FROM tbproduct WHERE ProdBarCode = varProdBarCode) THEN
				IF NOT EXISTS (SELECT CatName FROM tbcategoryprod WHERE CatName = varCatName) THEN
								INSERT INTO tbcategoryprod (CatName) VALUES (varCatName);
				END IF;
                
                INSERT INTO tbProduct (ProdBarCode, ProdName, ProdPrice, CatCode) VALUES (varProdBarCode, varProdName, varProdPrice, (SELECT CatCode FROM tbCategoryProd WHERE CatName = varCatName));
END IF;
END; 
$$ DELIMITER ;



DROP PROCEDURE IF EXISTS spInsertProductImage;

DELIMITER $$
CREATE PROCEDURE spInsertProductImage (varType varchar(25), varImage blob, varImgSize varchar(25), varImgTitle varchar(50), varProdBarCode int)
BEGIN
INSERT INTO tbImageProducts (ImgType, Image, ImgSize, ImgTitle, ProdBarCode) VALUES (varType, varImage, varImgSize, varImgTitle, varProdBarCode);
END; 
$$ DELIMITER ;



DROP PROCEDURE IF EXISTS spInsertOrderItem;
DELIMITER $$
CREATE PROCEDURE spInsertOrderItem (varProdBarCode int, varItemAmount int, varCustCPF varchar(14))
BEGIN
IF NOT EXISTS (SELECT OrdCode FROM tborder WHERE CustCPF = varCustCPF AND StatusOrder = 'U') THEN
				INSERT INTO tborder (CustCPF) VALUES (varCustCPF);
        END IF;
IF NOT EXISTS (SELECT varProdBarCode FROM tbOrderItem WHERE OrdCode = (SELECT OrdCode FROM tborder WHERE CustCPF = varCustCPF AND StatusOrder = 'U') AND ProdBarCode = varProdBarCode) THEN
		
				INSERT INTO tbOrderItem (OrdCode, ProdBarCode, ItemUnitaryPrice, ItemAmount, ItemTotalPrice)
                VALUES ((SELECT OrdCode FROM tborder WHERE CustCPF = varCustCPF AND StatusOrder = 'U'), varProdBarCode, (SELECT ProdPrice FROM tbproduct WHERE ProdBarCode = varProdBarCode), varItemAmount, (SELECT ProdPrice FROM tbproduct WHERE ProdBarCode = varProdBarCode));
                
                UPDATE tbOrderItem SET ItemTotalPrice = (varItemAmount * ItemUnitaryPrice) WHERE OrdCode = (SELECT OrdCode FROM tborder WHERE CustCPF = varCustCPF AND StatusOrder = 'U') AND ProdBarCode = varProdBarCode;
ELSE

UPDATE tbOrderItem SET ItemAmount =  varItemAmount WHERE OrdCode = (SELECT OrdCode FROM tborder WHERE CustCPF = varCustCPF AND StatusOrder = 'U') AND ProdBarCode = varProdBarCode;
UPDATE tbOrderItem SET ItemTotalPrice = (varItemAmount * ItemUnitaryPrice) WHERE OrdCode = (SELECT OrdCode FROM tborder WHERE CustCPF = varCustCPF AND StatusOrder = 'U') AND ProdBarCode = varProdBarCode;
END IF;
END; 
$$ DELIMITER ;



DROP PROCEDURE IF EXISTS spFinishOrder;
DELIMITER $$
CREATE PROCEDURE spFinishOrder(varPayOption varchar(20), varCustCPF varchar(14))
BEGIN
IF NOT EXISTS (SELECT OpPayCode FROM tbOptionPay WHERE PayOption = varPayOption) THEN
				INSERT INTO tbOptionPay (PayOption) VALUES (varPayOption);
END IF;
IF EXISTS (SELECT PayCode FROM tbOrder WHERE StatusOrder = 'U' AND CustCPF = varCustCPF) THEN
				INSERT INTO tbpayment (PayValue, OpPayCode, CustCPF) VALUES ((SELECT SUM(ItemTotalPrice) FROM tbOrderItem WHERE OrdCode = (SELECT OrdCode FROM tbOrder WHERE CustCPF = varCustCPF AND StatusOrder = 'U')), (SELECT OpPayCode FROM tbOptionPay WHERE PayOption = varPayOption), varCustCPF);
END IF;

UPDATE tbOrder SET OrdTotalPrice = (SELECT PayValue FROM tbpayment WHERE CustCPF = varCustCPF AND StatusPayment = 'U'), StatusOrder = 'F', PayCode = (SELECT PayCode FROM tbpayment WHERE CustCPF = varCustCPF AND StatusPayment = 'U') WHERE CustCPF = varCustCPF AND StatusOrder = 'U';
Update tbpayment SET StatusPayment = 'F' WHERE CustCPF = varCustCPF AND StatusPayment = 'U';
END; 
$$ DELIMITER ;


DROP PROCEDURE IF EXISTS spDeleteProductOrderItem;

DELIMITER $$
CREATE PROCEDURE spDeleteProductOrderItem(varCustCPF varchar(14), varProdBarCode int)
BEGIN
DELETE FROM tbOrderItem WHERE OrdCode = (SELECT OrdCode FROM tbOrder WHERE CustCPF = varCustCPF AND StatusOrder = 'U') AND ProdBarCode = varProdBarCode;
END
$$ DELIMITER ;



DROP PROCEDURE IF EXISTS spViewOrdersByCPF;

DELIMITER $$
CREATE PROCEDURE spViewOrdersByCPF (varCustCPF varchar(14))
BEGIN
select * From vwAllOrders WHERE CustCPF = varCustCPF;
END; 
$$ DELIMITER ;

DROP PROCEDURE IF EXISTS spInsertPackage;
DELIMITER $$
CREATE PROCEDURE spInsertPackage (varPackPrice decimal(8,2), varPackName varchar(150), varImage blob, varPackDescription varchar(300))
BEGIN
INSERT INTO tbpackage (PackPrice, PackName, Image, PackDescription)
VALUES(varPackPrice, varPackName, varImage, varPackDescription);
END; 
$$ DELIMITER ;



DROP PROCEDURE IF EXISTS spReserve;

DELIMITER $$
CREATE PROCEDURE spReserve (varResAmount int, varCustCPF varchar(14), varPackName varchar(150))
BEGIN
IF NOT EXISTS (SELECT ResCode FROM tbreserve WHERE CustCPF = varCustCPF AND StatusReserve = 'U') THEN
INSERT INTO tbreserve (ResPrice,ResAmount, CustCPF, PackCode)
VALUES ((SELECT PackPrice FROM tbPackage WHERE PackName = varPackName AND StatusPack = 'A') * varResAmount,varResAmount, varCustCPF, (SELECT PackCode FROM tbPackage WHERE PackName = varPackName AND StatusPack = 'A'));
ELSE 
UPDATE tbreserve SET ResPrice = (SELECT PackPrice FROM tbPackage WHERE PackName = varPackName AND StatusPack = 'A'), ResAmount = varResAmount, PackCode = (SELECT PackCode FROM tbPackage WHERE PackName = varPackName AND StatusPack = 'A') WHERE CustCPF = varCustCPF AND StatusReserve = 'U';
END IF;
END; 
$$ DELIMITER ;

DROP PROCEDURE IF EXISTS spInsertOrder;
DELIMITER $$
CREATE PROCEDURE spInsertOrder (varOrdDate date, varTotalPrice decimal(8,2), varStatusOrder char(1), varCustCPF varchar(14), varPayDate date, varIsDeleted char(1), varStatusPayment char(1), varPayOption varchar(20))
BEGIN
IF NOT EXISTS (SELECT OpPayCode FROM tbOptionPay WHERE PayOption = varPayOption) THEN
				INSERT INTO tbOptionPay (PayOption) VALUES (varPayOption);
END IF;
				insert tbPayment (PayDate, PayValue, IsDeleted, StatusPayment, OpPayCode, CustCPF) values(varPayDate, varTotalPrice, varIsDeleted, varStatusPayment, (SELECT OpPayCode FROM tbOptionPay WHERE PayOption = varPayOption), varCustCPF);
				insert tbOrder (OrdDate, OrdTotalPrice, StatusOrder, IsDeleted, Paycode, CustCPF) values(varOrdDate, varTotalPrice, varStatusOrder, varIsDeleted, (SELECT PayCode from tbPayment where CustCPF = varCustCPF ORDER BY PayCode DESC LIMIT 1), varCustCPF);

END; 
$$ DELIMITER ;

SELECT OpPayCode FROM tbOptionPay WHERE PayOption = 'Débito';

DROP PROCEDURE IF EXISTS spInsertOrderItem;
DELIMITER $$
CREATE PROCEDURE spInsertOrderItem (varProdBarCode int, varItemUnitaryPrice decimal(8,2), varItemAmount int,  varTotalPrice decimal(8,2), varCustCPF varchar(14))
BEGIN
				insert tbOrderItem (OrdCode, ProdBarCode, ItemUnitaryPrice, ItemAmount, ItemTotalPrice) values((SELECT OrdCode from tbOrder where CustCPF = varCustCPF ORDER BY OrdCode DESC LIMIT 1) ,varProdBarCode, varItemUnitaryPrice, varItemAmount, varTotalPrice);

END; 
$$ DELIMITER ;

DROP PROCEDURE IF EXISTS spInsertReserve;
DELIMITER $$
CREATE PROCEDURE spInsertReserve (varPayDate date, varPayValue decimal(8,2), varIsDeleted char(1), varStatusPayment char(1), varPayOption varchar(20), varCustCPF varchar(14), varResValidity date, varResAmount int, varPackCode int)
BEGIN
IF NOT EXISTS (SELECT OpPayCode FROM tbOptionPay WHERE PayOption = varPayOption) THEN
				INSERT INTO tbOptionPay (PayOption) VALUES (varPayOption);
END IF;
				insert tbPayment (PayDate, PayValue, IsDeleted, StatusPayment, OpPayCode, CustCPF) values(varPayDate, varPayValue, varIsDeleted, varStatusPayment, (SELECT OpPayCode FROM tbOptionPay WHERE PayOption = varPayOption), varCustCPF);
				insert tbReserve (ResValidity, ResPrice, ResAmount, IsDeleted, CustCPF, PackCode, PayCode) values(varResValidity, varPayValue, varResAmount, varIsDeleted, varCustCPF, varPackCode, (SELECT PayCode from tbPayment where CustCPF = varCustCPF ORDER BY PayCode DESC LIMIT 1));

END; 
$$ DELIMITER ;

Call spInsertReserve('2021-12-12',55.55,'N','U','Crédito','54588519867','2020-01-12',1,1);

DROP PROCEDURE IF EXISTS spFinishReserve;
DELIMITER $$
CREATE PROCEDURE spFinishReserve (varPayOption varchar(20), varCustCPF varchar(14))
BEGIN
IF NOT EXISTS (SELECT OpPayCode FROM tbOptionPay WHERE PayOption = varPayOption) THEN
				INSERT INTO tbOptionPay (PayOption) VALUES (varPayOption);
END IF;

IF EXISTS (SELECT PayCode FROM tbreserve WHERE CustCPF = varCustCPF AND StatusReserve = 'U') THEN
INSERT INTO tbpayment (PayValue, OpPayCode, CustCPF) VALUES ((SELECT ResPrice FROM tbReserve WHERE CustCPF = varCustCPF AND StatusReserve = 'U'), (SELECT OpPayCode FROM tbOptionPay WHERE PayOption = varPayOption), varCustCPF);
END IF;
UPDATE tbreserve SET StatusReserve = 'F', PayCode = (SELECT PayCode FROM tbpayment WHERE CustCPF = varCustCPF AND StatusPayment = 'U') WHERE CustCPF = varCustCPF AND StatusReserve = 'U';
UPDATE tbpayment SET StatusPayment = 'F' WHERE CustCPF = varCustCPF AND StatusPayment = 'U';
END; 
$$ DELIMITER ;


/*CALL PROCEDURES*/


/* Insert Customer */


CALL spInsertCustomer(44687871801, "Gabriel Cerruti", '2003-02-25', "M", 11975466558, "bielhcsousa@gmail.com", "123321", 65, 05133160);
CALL spInsertCustomer(44687871822, "Gabriel Cerruti", '2003-02-25', "M", 11975466522, "bielhcsou22@gmail.com", "123322", 70, 05133122);
CALL spInsertCustomer(45665465444, "Fernanda Rocha", '2003-02-25', "F", 11975465522, "fefes@gmail.com", "123322", 70, 05133122);
CALL spInsertCustomer(54588519867, "Micael Rodrigues", '2003-09-11', "M", 11000065522, "micaelrodrigues504@gmail.com", "123456", 47, 05358150);



CALL spLoginUser("bielhcsou22@gmail.com", "123322");

/* Insert Employee */
CALL spInsertEmployee("44697971801", "Gabriel Cerruti", "Cerriti", '2003-02-25', "m", "1197546558", "bielhousa@gmail.com", "123321", 12, "02522-100", "Gerente");

/* Insert Products */
CALL spInsertProduct (123654656, "Shampo Pantene", 24.59, "Shampoo");
CALL spInsertProduct (123600006, "Shampo Johnson's", 17.59, "Shampoo");
CALL spInsertProduct (123609999, "Creme Pimenta Negra", 56.51, "Creme");
CALL spInsertProduct (123607999, "Creme Reduxcel", 70.00, "Creme");
CALL spInsertProduct (555555555, "Pente Santa Clara", 2.53, "Pente");

CALL spInsertProductImage ("ImageProduct", load_file('C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/shampo_pantene.jpg'),"Medium", "Shammpo Pantene", 123654656);
CALL spInsertProductImage ("ImageProduct", load_file('C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/shampo_johnson.jpg'),"Medium", "Shampo Johnson's", 123600006);
CALL spInsertProductImage ("ImageProduct", load_file('C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/creme_de_massagem_pimenta_negra.jpg'),"Medium", "Creme de massage pimenta negra", 123609999);
CALL spInsertProductImage ("ImageProduct", load_file('C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/Creme_Reduxcel.jpg'),"Medium", "Creme Reduxcel", 123607999);
CALL spInsertProductImage ("ImageProduct", load_file('C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/pente_santa_clara.jpg'),"Medium", "Pente Santa CLara", 555555555);



/* Add product in cart */
/*
CALL spInsertOrderItem (123609999, 4, 45665465444);
CALL spInsertOrderItem (123600006, 4, 45665465444);
CALL spInsertOrderItem (123607999, 3, 45665465444);
CALL spInsertOrderItem (555555555, 3, 45665465444);
*/
/* Finish Order and close payment */
/*
CALL spFinishOrder ("Crédito", 45665465444);
*/
/* Insert Package*/
CALL spInsertPackage(565.65, "Sonho Feliz",load_file('C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/bob_toy.jpg') , "Um dia de massagem :)");

/* Make reserve */
/*
CALL spReserve(4, 44687871801, 'Sonho Feliz');
CALL spReserve(3, 45665465444, 'Sonho Feliz');
*/

/*Finish Reserve and close payment*/
/*
CALL spFinishReserve ("Débito", "44687871801");

CALL spDeleteProductOrderItem(44687871822, 555555555);

CALL spViewOrdersByCPF (44687871801);
*/
CREATE VIEW SelectProduct AS
SELECT
    ProdBarCode,
    ProdName,
    ProdPrice,
    /*Image,*/
    IsDeleted,
    CatCode,
    CatName
FROM
    tbProduct
Left JOIN tbCategoryProd USING (CatCode);
/*Inner Join tbImageProducts using (ProdBarCode);*/

select * from tbProduct;
/*drop view selectProduct;*/
Select * from SelectProduct;

USE dbLotus;

CREATE VIEW SelectPackage AS
SELECT
    PackCode,
    PackPrice,
    Image,
    PackName,
    PackDescription
FROM
    tbPackage;


CREATE VIEW SelectCustomer AS
SELECT
    CustName,
    DATE_FORMAT(CustDtNasc, '%d/%m/%Y') as CustDtNasc,
    CustGender,
    CustTel,
    CustEmail,
    CustPassword,
    CustCPF,
    CustNumberAddress,
    IsDeleted,
    CEPAddress
FROM
    tbCustomer;
 
 select * from SelectPackage where PackCode = 1;
/*drop view SelectPackage;*/
Select * from SelectPackage;
 /*
CREATE VIEW SelectPayments AS
SELECT
    PayValue,
    DATE_FORMAT(PayDate, '%d/%m/%Y') as PayDate,
    StatusPayment,
    OpPayCode,
    OrdTotalPrice,
	DATE_FORMAT(ResValidity, '%d/%m/%Y') as ResValidity,
    ResAmount,
    CustCPF
FROM
    tbCustomer
Left JOIN tbOrder USING (OrdCode)
Left Join tbReserve Using (ResCode);
*/
CREATE VIEW SelectOrder AS
SELECT
    OrdCode,
    DATE_FORMAT(OrdDate, '%d/%m/%Y') as OrdDate,
    OrdTotalPrice As TotalPrice,
    StatusOrder,
    PayCode,
	CustEmail,
    tbOrder.CustCPF,
    PayOption
FROM
    tbOrder
Left JOIN tbPayment Using (PayCode)
Left JOIN tbOptionPay Using (OpPayCode)
Left JOIN tbCustomer ON tbCustomer.CustCPF = tbOrder.CustCPF
;

CREATE VIEW SelectReserve AS
SELECT
    ResCode,
    DATE_FORMAT(ResValidity, '%d/%m/%Y') as ResValidity,
    PayValue,
    ResAmount,
    tbReserve.IsDeleted,
    tbReserve.CustCPF,
    CustEmail,
    PayOption,
    StatusPayment
FROM
    tbReserve
Left JOIN tbPayment Using (PayCode)
Left JOIN tbOptionPay Using (OpPayCode)
Left JOIN tbCustomer ON tbCustomer.CustCPF = tbReserve.CustCPF
; 
/*
select * from SelectOrder;
select * from tbOrder;
select * from SelectPackage where PackCode = 1;
*/
/*
drop view SelectCustomer;
drop view SelectReserve;
drop view SelectOrder;
use dbLotus
*/
/*
Select * from SelectCustomer;
Select * from SelectProduct;
Select * from SelectReserve Where StatusPayment = 'U';
Select * from SelectReserve Where CustEmail = 'micaelrodrigues504@gmail.com';
Select * from SelectOrder Where CustEmail = 'micaelrodrigues504@gmail.com';
select * from SelectOrder where  StatusOrder = 'U';
*/

USE dbLotus;


