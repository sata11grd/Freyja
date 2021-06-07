var i=1;
var j=2;
var ret = Oram.init(1);
print("return from Oram.init:" + ret);
if(i!=0) { print(i+j); }
ret = Oram.init(0);
for(var x=0; x<10; x++){Oram.search(x); print("return from Oram.search:" + ret);}
print(x);
print(Math.exp(3));
ret = ML.test(2,3,4);
print("return from ML.test:" + ret);
