% https://atcoder.jp/contests/agc020/tasks/agc020_a

read_string(Str) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, Str).

read_number(Num) :-
  read_string(Str),
  number_string(Num, Str).

result_a('Alice').
result_b('Borys').

solve(A, B, Ret) :-
  D is B - A,
  D mod 2 =:= 0 ->
    result_a(Ret);
    result_b(Ret).

main :-
  read_number(N),
  read_number(A),
  read_number(B),
  solve(A, B, Ret),
  writeln(Ret).

:- main.

% https://atcoder.jp/contests/agc020/submissions/17658923
