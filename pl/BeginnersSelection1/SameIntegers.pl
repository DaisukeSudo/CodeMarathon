% https://atcoder.jp/contests/abc093/tasks/arc094_a

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

action([A, A, A], R, R).
action([A, B, B], Q, R) :-
  A2 is A + 2,
  next(A2, B, B, Q, R).
action([A, B, C], Q, R) :-
  A1 is A + 1,
  B1 is B + 1,
  next(A1, B1, C, Q, R).

next(A, B, C, Q, R) :-
  sort(0, @=<, [A, B, C], Sorted),
  Q1 is Q + 1,
  action(Sorted, Q1, R).

solve(A, B, C, Ret) :-
  next(A, B, C, -1, Ret).

main :-
  read_number(A),
  read_number(B),
  read_number(C),
  solve(A, B, C, Ret),
  writeln(Ret).

:- main.

% https://atcoder.jp/contests/abc093/submissions/17586483
