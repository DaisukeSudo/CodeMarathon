% https://atcoder.jp/contests/abc081/tasks/arc086_a

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

take_number(0, _, 0) :- !.
take_number(_, [], 0) :- !.
take_number(N, [X|Xs], C) :-
  N1 is N - 1,
  take_number(N1, Xs, C1),
  C is C1 + X.

solve(N, K, As, Ret) :-
  count_by_group(As, Dict),
  dict_values(Dict, Counts),
  length(Counts, Size),
  ChangeCount is max(0, Size - K),
  sort(0, @=<, Counts, Sorted),
  take_number(ChangeCount, Sorted, Ret).

main :-
  read_number(N),
  read_number(K),
  read_numbers(N, As),
  solve(N, K, As, Ret),
  write(Ret).

:- main.

% https://atcoder.jp/contests/abc081/submissions/17326076 (TLE)
% https://atcoder.jp/contests/abc081/submissions/17328394 (TLE)
