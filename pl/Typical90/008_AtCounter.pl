% https://atcoder.jp/contests/typical90/tasks/typical90_h

read_string(Str) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, Str).

read_number(Num) :-
  read_string(Str),
  number_string(Num, Str).

read_char(Char) :-
  current_input(Input),
  read_string(Input, 1, Char).

read_chars(0, []).
read_chars(N, [X|Xs]) :-
  read_char(X),
  N1 is N - 1,
  read_chars(N1, Xs).


step("a", [A, T, C, O, D, E, R], [X, T, C, O, D, E, R]) :- X is  A + 1.
step("t", [A, T, C, O, D, E, R], [A, X, C, O, D, E, R]) :- X is (T + A) mod 1000000007.
step("c", [A, T, C, O, D, E, R], [A, T, X, O, D, E, R]) :- X is (C + T) mod 1000000007.
step("o", [A, T, C, O, D, E, R], [A, T, C, X, D, E, R]) :- X is (O + C) mod 1000000007.
step("d", [A, T, C, O, D, E, R], [A, T, C, O, X, E, R]) :- X is (D + O) mod 1000000007.
step("e", [A, T, C, O, D, E, R], [A, T, C, O, D, X, R]) :- X is (E + D) mod 1000000007.
step("r", [A, T, C, O, D, E, R], [A, T, C, O, D, E, X]) :- X is (R + E) mod 1000000007.
step(_,   [A, T, C, O, D, E, R], [A, T, C, O, D, E, R]).

loop([], [_, _, _, _, _, _, R], R).
loop([H|Ts], P, Ret) :-
  step(H, P, N),
  loop(Ts, N, Ret).

solve([], 0).
solve(Xs, Ret) :-
  loop(Xs, [0, 0, 0, 0, 0, 0, 0], Ret).

main :-
  read_number(N),
  read_chars(N, Xs),
  solve(Xs, Ret),
  write(Ret).

:- main.

% https://atcoder.jp/contests/typical90/submissions/31949353
