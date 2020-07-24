% https://atcoder.jp/contests/abs/tasks/abc081_a

% number_char(Num, Char) :-
%   string_chars(Str, [Char]),
%   number_string(Num, Str).

% main :-
%   get_char(S1),
%   get_char(S2),
%   get_char(S3),
%   number_char(N1, S1),
%   number_char(N2, S2),
%   number_char(N3, S3),
%   Ret is N1 + N2 + N3,
%   write(Ret).

% :- main.

% https://atcoder.jp/contests/abs/submissions/15390163

% ----

read_string(Str) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, Str).

number_char(Num, Char) :-
  string_chars(Str, [Char]),
  number_string(Num, Str).

main :-
  read_string(S),
  string_chars(S, Chars),
  convlist([Char, Num] >> number_char(Num, Char), Chars, Nums),
  foldl(plus, Nums, 0, Ret),
  write(Ret).

:- main.

% https://atcoder.jp/contests/abs/submissions/15390370
