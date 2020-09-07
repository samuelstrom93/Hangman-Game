create table player (
	id SERIAL PRIMARY KEY,
	name varchar(255) UNIQUE NOT NULL
);

create table word (
	id SERIAL PRIMARY KEY,
	name varchar(255) UNIQUE NOT NULL
);

create table game (
	id SERIAL PRIMARY KEY,
	is_won BOOLEAN NOT NULL,
	number_of_tries INT NOT NULL,
	start_time TIMESTAMP NOT NULL,
	end_time TIMESTAMP NOT NULL,
	number_of_incorrect_tries INT NOT NULL,
	player_id INT,
	word_id INT,
	FOREIGN KEY (player_id) REFERENCES player(id),
	FOREIGN KEY (word_id) REFERENCES word(id)
);