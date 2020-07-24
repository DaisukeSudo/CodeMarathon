% ----- 入力 -----

read_string(String) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, String).

read_number(Number) :-
  read_string(String),
  number_string(Number, String).
