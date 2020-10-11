% ----- 入力 -----

read_string(Str) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, Str).

read_strings(0, []).
read_strings(N, [X|Xs]) :-
  read_string(X),
  N1 is N - 1,
  read_strings(N1, Xs).

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

string_numbers(Str, Nums) :-
  string_chars(Str, Chars),
  convlist([Char, Num] >> number_char(Num, Char), Chars, Nums).


% ----- カウント -----

count_in_list(N, [], C, C).
count_in_list(N, [X|Xs], A, C) :-
  (N =:= X -> A1 is A + 1; A1 is A),
  count_in_list(N, Xs, A1, C).
count_in_list(N, List, C) :-
  count_in_list(N, List, 0, C).

count_by_group([], _, Cs, Cs).
count_by_group([X|Xs], List, As, Cs) :-
  writeln(([X|Xs], List, As)),
  count_in_list(X, List, A),
  count_by_group(Xs, List, [A|As], Cs).
count_by_group(Xs, List, [A|As], Cs).
count_by_group(List, C) :-
  sort(0, @>, List, Distincted),
  count_by_group(Distincted, List, [], C).
