CREATE TABLE data_protection_key
(
  id serial NOT NULL,
  name character varying(1024) NOT NULL,
  value text NOT NULL,
  CONSTRAINT data_protection_key_pkey PRIMARY KEY (id)
)
