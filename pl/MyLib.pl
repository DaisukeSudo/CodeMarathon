% ----- 入力 -----

read_char(Char) :-
  current_input(Input),
  read_string(Input, 1, Char).

read_chars(0, []).
read_chars(N, [X|Xs]) :-
  read_char(X),
  N1 is N - 1,
  read_chars(N1, Xs).

read_chars2(0, _, []).
read_chars2(H, W, [Xs|Xss]) :-
  read_chars(W, Xs),
  read_char(N),
  H1 is H - 1,
  read_chars2(H1, W, Xss).

read_string(Str) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, Str).

read_strings(0, []).
read_strings(N, [X|Xs]) :-
  read_string(X),
  N1 is N - 1,
  read_strings(N1, Xs).

read_strings2(0, _, []).
read_strings2(H, W, [Xs|Xss]) :-
  read_strings(W, Xs),
  H1 is H - 1,
  read_strings2(H1, W, Xss).

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

write_line([]).
write_line([S|Ss]) :-
  write(S),
  write_line(Ss).

write_lines([]).
write_lines([Ss|Sss]) :-
  write_line(Ss),
  writeln(""),
  write_lines(Sss).


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


% ----- リスト -----

combinePair([], _, []) :- !.
combinePair(_, [], []) :- !.
combinePair([A|As], Bs, Css) :-
  combinePair1(A, Bs, Cs1),
  combinePair(As, Bs, Cs2),
  append(Cs1, Cs2, Css).

combinePair1(_, [], []).
combinePair1(A, [B|Bs], [C|Cs]) :-
  C = (A, B),
  combinePair1(A, Bs, Cs).

map(_P, [], []).
map(P, [H1|Ts1], [H2|Ts2]) :-
  call(P, H1, H2),
  map(P, Ts1, Ts2).

foldl(P, [H|Ts], A, R) :-
  call(P, A, H, A2),
  foldl(P, Ts, A2, R).
foldl(_P, [], R, R).

reduce(P, [H|Ts], R) :-
  foldl(P, Ts, H, R).

unique([], []).
unique([H|Ts1], [H|Ts2]) :-
  delete(Ts1, H, Vs),
  unique(Vs, Ts2).


% ----- 関数 -----

gcd(A, 0, A).
gcd(A, B, R) :- B > 0, M is A mod B, gcd(B, M, R).

lcm(A, B, R) :- gcd(A, B, G), R is A * B / G.
