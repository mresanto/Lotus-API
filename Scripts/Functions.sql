
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

DELIMITER $$
DROP FUNCTION IF EXISTS F_Validation_Cad;
CREATE FUNCTION F_Validation_Cad(v_email VARCHAR(300), v_password varchar(50))
RETURNS VARCHAR(100)
DETERMINISTIC
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
  RETURN MSG;
  
END$$
DELIMITER ;


DELIMITER $$
DROP FUNCTION IF EXISTS F_Validation_Cad;
CREATE FUNCTION F_Validation_Cad(v_cpf varchar(14),v_email VARCHAR(300))
RETURNS VARCHAR(100)
DETERMINISTIC
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
DELIMITER ;
