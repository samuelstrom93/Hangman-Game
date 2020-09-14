create table player (
	id SERIAL PRIMARY KEY,
	name varchar(255) UNIQUE NOT NULL
);

create table word (
	id SERIAL PRIMARY KEY,
	name varchar(255) UNIQUE NOT NULL,
	hint varchar(255) NOT NULL
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

create table message (
	id SERIAL PRIMARY KEY,
	topic varchar(255) NOT NULL,
	content varchar(255) NOT NULL,
	sent_at TIMESTAMP NOT NULL,
	read_at TIMESTAMP,
	sender_id INT NOT NULL,
	reciever_id INT NOT NULL,
	FOREIGN KEY (sender_id) REFERENCES player(id),
	FOREIGN KEY (reciever_id) REFERENCES player(id)
)