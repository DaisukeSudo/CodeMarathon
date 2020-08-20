% ----- 入力 -----

read_string(Str) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, Str).

read_number(Num) :-
  read_string(Str),
  number_string(Num, Str).

read_numbers(0, []).
read_numbers(N, [X|Xs]) :-
  read_number(X),
  N1 is N - 1,
  read_numbers(N1, Xs).


% ----- 変換 -----

number_char(Num, Char) :-
  string_chars(Str, [Char]),
  number_string(Num, Str).
