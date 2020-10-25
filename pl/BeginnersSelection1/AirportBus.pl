% https://atcoder.jp/contests/agc011/tasks/agc011_a

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

ride_on_a_bus(_, _, _, [], _, R, R).
ride_on_a_bus(C, K, T1, [T|Ts], B, A, R) :-
  T < T1 + K,
  B < C,
  B1 is B + 1,
  ride_on_a_bus(C, K, T1, Ts, B1, A, R).
ride_on_a_bus(C, K, _, [T|Ts], _, A, R) :-
  A1 is A + 1,
  ride_on_a_bus(C, K, T, Ts, 1, A1, R).
ride_on_a_bus(C, K, [T|Ts], Ret) :-
  ride_on_a_bus(C, K, T, Ts, 1, 1, Ret).

solve(C, K, Ts, Ret) :-
  sort(0, @=<, Ts, Sorted),
  ride_on_a_bus(C, K, Sorted, Ret).

main :-
  read_number(N),
  read_number(C),
  read_number(K),
  read_numbers(N, Ts),
  solve(C, K, Ts, Ret),
  writeln(Ret).

:- main.

% https://atcoder.jp/contests/agc011/submissions/17575782
