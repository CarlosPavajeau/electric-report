CREATE TABLE electric.`lines`
(
    `id`   INT AUTO_INCREMENT,
    `name` VARCHAR(50),
    PRIMARY KEY (`id`)
);

CREATE TABLE electric.`customer_type`
(
    `id`   INT AUTO_INCREMENT,
    `name` VARCHAR(50),
    PRIMARY KEY (`id`)
);

CREATE TABLE electric.`maintenance_evaluation`
(
    `id`          INT AUTO_INCREMENT,
    `date`        DATE,
    `line_id`     INT,
    `consumption` DECIMAL(10, 2),
    `loss`        DECIMAL(10, 2),
    `cost`        DECIMAL(10, 2),
    PRIMARY KEY (`id`),
    FOREIGN KEY (`line_id`) REFERENCES `lines` (`id`)
);

CREATE TABLE electric.`maintenance_customer`
(
    `id`               INT AUTO_INCREMENT,
    `date`             DATE,
    `line_id`          INT,
    `customer_type_id` INT,
    `consumption`      DECIMAL(10, 2),
    `loss`             DECIMAL(10, 2),
    `cost`             DECIMAL(10, 2),
    PRIMARY KEY (`id`),
    FOREIGN KEY (`line_id`) REFERENCES `lines` (`id`),
    FOREIGN KEY (`customer_type_id`) REFERENCES `customer_type` (`id`)
);

create procedure electric.GetConsumptionHistoryByCustomerType(IN startDate date, IN endDate date)
BEGIN
SELECT ct.name AS customerType,
       l.name  AS line,
       mc.date,
       mc.consumption,
       mc.loss,
       mc.cost
FROM maintenance_customer AS mc
         JOIN `lines` l on l.id = mc.line_id
         JOIN customer_type ct on ct.id = mc.customer_type_id
WHERE mc.date BETWEEN startDate AND endDate
ORDER BY ct.name, l.name, mc.date;
END;

create procedure electric.GetConsumptionHistoryByLine(IN startDate date, IN endDate date)
BEGIN
SELECT l.name AS `line`,
       e.date,
       e.consumption,
       e.loss,
       e.cost
FROM maintenance_evaluation AS e
         JOIN `lines` AS l ON e.line_id = l.id
WHERE e.date BETWEEN startDate AND endDate
ORDER BY l.name, e.date;
END;
    
create procedure electric.GetTop20WorstLinesByCustomer(IN startDate date, IN endDate date)
BEGIN
SELECT ct.name      AS customerType,
       l.name       AS line,
       SUM(mc.loss) AS totalLoss
FROM maintenance_customer AS mc
         JOIN customer_type AS ct ON mc.customer_type_id = ct.id
         JOIN `lines` AS l ON mc.line_id = l.id
WHERE mc.date BETWEEN startDate AND endDate
GROUP BY ct.name, l.name
ORDER BY totalLoss DESC LIMIT 20;
END;
