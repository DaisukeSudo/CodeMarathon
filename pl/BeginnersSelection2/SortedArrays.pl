% https://atcoder.jp/contests/agc013/tasks/agc013_a

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

next(P, C,  0, -1, 0) :- P > C.
next(P, C,  0,  1, 0) :- P < C.
next(P, C,  1,  0, 1) :- P > C.
next(P, C, -1,  0, 1) :- P < C.
next(_, _,  T,  T, 0).

progressive(_, [], _, A, A).
progressive(P, [X|Xs], T, A, Ret) :-
  next(P, X, T, T2, I),
  A1 is A + I,
  progressive(X, Xs, T2, A1, Ret).

solve([], 1).
solve([X|Xs], Ret) :-
  progressive(X, Xs, 0, 1, Ret).

main :-
  read_number(N),
  read_numbers(N, As),
  solve(As, Ret),
  writeln(Ret).

:- main.

% https://atcoder.jp/contests/agc013/submissions/17546248
