CREATE TABLE cereals (
    name varchar,
    mfr varchar,
    type varchar,
    calories float,
    protein float,
    fat float,
    sodium float,
    fiber float,
    carbo float,
    sugars float,
    potass float,
    vitamins float,
    shelf float,
    weight float,
    cups float,
    rating float
);

COPY cereals FROM '/workspace/database/cereals.csv' DELIMITER ',' CSV HEADER;

ALTER TABLE cereals ADD id serial PRIMARY KEY;
ALTER TABLE cereals ADD deleted boolean;
ALTER TABLE cereals ALTER COLUMN deleted SET DEFAULT false;
