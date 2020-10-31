% https://atcoder.jp/contests/abc073/tasks/abc073_c

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

joisino([], _, E, Q, R):-
  R is Q + E.
joisino([A|As], P, 0, Q, R) :-
  A =:= P,
  joisino(As, A, 1, Q, R).
joisino([A|As], P, 1, Q, R) :-
  A =:= P,
  joisino(As, A, 0, Q, R).
joisino([A|As], P, E, Q, R) :-
  A =\= P,
  Q1 is Q + E,
  joisino(As, A, 1, Q1, R).

solve(As, Ret) :-
  sort(0, @=<, As, Sorted),
  joisino(Sorted, 0, 0, 0, Ret).

main :-
  read_number(N),
  read_numbers(N, As),
  solve(As, Ret),
  writeln(Ret).

:- main.

% https://atcoder.jp/contests/abc073/submissions/17746392
