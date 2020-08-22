% https://atcoder.jp/contests/abs/tasks/abc085_b

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

solve([], 0).
solve(Ds, Ret) :-
  sort(Ds, Sorted),
  length(Sorted, Ret).

main :-
  read_number(N),
  read_numbers(N, Ds),
  solve(Ds, Ret),
  write(Ret).

:- main.

% https://atcoder.jp/contests/abs/submissions/16086472
