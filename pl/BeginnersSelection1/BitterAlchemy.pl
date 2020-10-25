% https://atcoder.jp/contests/abc095/tasks/abc095_b

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

solve(N, X, Ms, Ret) :-
  sumlist(Ms, Sum),
  Remain is X - Sum,
  min_list(Ms, Min),
  divmod(Remain, Min, N2, _),
  Ret is N + N2.

main :-
  read_number(N),
  read_number(X),
  read_numbers(N, Ms),
  solve(N, X, Ms, Ret),
  writeln(Ret).

:- main.

% https://atcoder.jp/contests/abc095/submissions/17118837
