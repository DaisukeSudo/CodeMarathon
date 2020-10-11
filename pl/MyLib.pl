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

read_numbers2(0, _, []).
read_numbers2(H, W, [Xs|Xss]) :-
  read_numbers(W, Xs),
  H1 is H - 1,
  read_numbers2(H1, W, Xss).


% ----- 出力 -----

format_result(0, 'Even').
format_result(1, 'Odd').

result_y('Yes').
result_n('No').


% ----- 変換 -----

number_char(Num, Char) :-
  string_chars(Str, [Char]),
  number_string(Num, Str).

string_numbers(Str, Nums) :-
  string_chars(Str, Chars),
  convlist([Char, Num] >> number_char(Num, Char), Chars, Nums).


% ----- カウント -----

digits_number(Num, Len) :-
  number_string(Num, Str),
  string_length(Str, Len).

count_in_list(N, [], C, C).
count_in_list(N, [X|Xs], A, C) :-
  (N =:= X -> A1 is A + 1; A1 is A),
  count_in_list(N, Xs, A1, C).
count_in_list(N, List, C) :-
  count_in_list(N, List, 0, C).

count_by_group([], DictR, DictR).
count_by_group([X|Xs], DictA, DictR) :-
  (get_dict(X, DictA, V) -> V1 is V + 1; V1 is 1),
  put_dict(X, DictA, V1, DictA1),
  count_by_group(Xs, DictA1, DictR).
count_by_group(List, DictR) :-
  count_by_group(List, dict{}, DictR).

dict_values([], _, VsR, VsR).
dict_values([K|Ks], Dict, VsA, VsR) :-
  get_dict(K, Dict, V),
  dict_values(Ks, Dict, [V|VsA], VsR).
dict_values(Dict, Values) :-
  dict_keys(Dict, Keys),
  dict_values(Keys, Dict, [], Values).
