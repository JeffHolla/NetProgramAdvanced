ÃS
[C:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\Program.cs
	namespace 	
CartService
 
{ 
internal 
class 
Program 
{ 
private 
static 
IServiceProvider '
_serviceProvider( 8
;8 9
static 
void 
Main 
( 
string 
[  
]  !
args" &
)& '
{ 	
var 
builder 
= 
WebApplication (
.( )
CreateBuilder) 6
(6 7
args7 ;
); <
;< =
var "
appSettingsEnvironment &
=' (
Environment) 4
.4 5"
GetEnvironmentVariable5 K
(K L
$strL d
)d e
;e f
builder 
. 
Configuration !
.! "
SetBasePath" -
(- .
	Directory. 7
.7 8
GetCurrentDirectory8 K
(K L
)L M
)M N
. 
AddJsonFile 
( 
$str -
,- .
optional/ 7
:7 8
false9 >
)> ?
. 
AddJsonFile 
( 
$" 
$str )
{) *"
appSettingsEnvironment* @
}@ A
$strA F
"F G
,G H
optionalI Q
:Q R
falseS X
)X Y
;Y Z
builder 
. 
Services 
. 
AddServices (
(( )
builder) 0
.0 1
Configuration1 >
)> ?
;? @
builder 
. 
Services 
. 
AddControllers +
(+ ,
), -
. 
AddJsonOptions 
(  
options  '
=>( *
{   
options!! 
.!! !
JsonSerializerOptions!! 1
.!!1 2"
DefaultIgnoreCondition!!2 H
=!!I J
JsonIgnoreCondition!!K ^
.!!^ _
WhenWritingNull!!_ n
;!!n o
options"" 
."" !
JsonSerializerOptions"" 1
.""1 2
ReferenceHandler""2 B
=""C D
ReferenceHandler""E U
.""U V
IgnoreCycles""V b
;""b c
}## 
)## 
;## 
builder%% 
.%% 
Services%% 
.%% 
AddVersioning%% *
(%%* +
)%%+ ,
;%%, -
builder&& 
.&& 
Services&& 
.&& 

AddSwagger&& '
(&&' (
)&&( )
;&&) *
builder(( 
.(( 
Services(( 
.(( #
ConfigureAuthentication(( 4
(((4 5
)((5 6
;((6 7
builder)) 
.)) 
Services)) 
.)) "
ConfigureAuthorization)) 3
())3 4
)))4 5
;))5 6
var,, 
app,, 
=,, 
builder,, 
.,, 
Build,, #
(,,# $
),,$ %
;,,% &
_serviceProvider-- 
=-- 
app-- "
.--" #
Services--# +
;--+ ,#
ConfigureQueueListening// #
(//# $
app//$ '
)//' (
;//( )
ConfigurePipeline11 
(11 
app11 !
)11! "
;11" #
app33 
.33 
Run33 
(33 
)33 
;33 
}44 	
internal66 
static66 
void66 
ConfigurePipeline66 .
(66. /
WebApplication66/ =
app66> A
)66A B
{77 	
if99 
(99 
app99 
.99 
Environment99 
.99  
IsDevelopment99  -
(99- .
)99. /
)99/ 0
{:: 
app;; 
.;; 

UseSwagger;; 
(;; 
);;  
;;;  !
app<< 
.<< 
UseSwaggerUI<<  
(<<  !
options<<! (
=><<) +
{== 
foreach>> 
(>> 
var>>  
description>>! ,
in>>- /
app>>0 3
.>>3 4
DescribeApiVersions>>4 G
(>>G H
)>>H I
)>>I J
{?? 
var@@ 
url@@ 
=@@  !
$"@@" $
$str@@$ -
{@@- .
description@@. 9
.@@9 :
	GroupName@@: C
}@@C D
$str@@D Q
"@@Q R
;@@R S
varAA 
nameAA  
=AA! "
descriptionAA# .
.AA. /
	GroupNameAA/ 8
.AA8 9
ToUpperInvariantAA9 I
(AAI J
)AAJ K
;AAK L
optionsBB 
.BB  
SwaggerEndpointBB  /
(BB/ 0
urlBB0 3
,BB3 4
nameBB5 9
)BB9 :
;BB: ;
}CC 
}DD 
)DD 
;DD 
appFF 
.FF %
UseDeveloperExceptionPageFF -
(FF- .
)FF. /
;FF/ 0
}GG 
ifII 
(II 
appII 
.II 
EnvironmentII 
.II  
IsProductionII  ,
(II, -
)II- .
)II. /
{JJ 
appKK 
.KK 
UseExceptionHandlerKK '
(KK' (
$strKK( 0
)KK0 1
;KK1 2
appLL 
.LL 
UseHstsLL 
(LL 
)LL 
;LL 
}MM 
appOO 
.OO 
UseHttpsRedirectionOO #
(OO# $
)OO$ %
;OO% &
appQQ 
.QQ 
UseAuthenticationQQ !
(QQ! "
)QQ" #
;QQ# $
appRR 
.RR 
UseAuthorizationRR  
(RR  !
)RR! "
;RR" #
appSS 
.SS 
UseIdentityLoggingSS "
(SS" #
)SS# $
;SS$ %
appUU 
.UU 
MapControllersUU 
(UU 
)UU  
.VV  
RequireAuthorizationVV $
(VV$ %
)VV% &
;VV& '
}WW 	
privateYY 
staticYY 
voidYY #
ConfigureQueueListeningYY 3
(YY3 4
WebApplicationYY4 B
appYYC F
)YYF G
{ZZ 	
var[[ 
queueOptions[[ 
=[[ 
app[[ "
.[[" #
Services[[# +
.[[+ ,

GetService[[, 6
<[[6 7
IOptions[[7 ?
<[[? @
CartQueueOptions[[@ P
>[[P Q
>[[Q R
([[R S
)[[S T
;[[T U
var\\ 
client\\ 
=\\ 
app\\ 
.\\ 
Services\\ %
.\\% &

GetService\\& 0
<\\0 1
IQueueClient\\1 =
>\\= >
(\\> ?
)\\? @
;\\@ A
client]] 
.]] 
	QueueName]] 
=]] 
queueOptions]] +
.]]+ ,
Value]], 1
.]]1 2
	QueueName]]2 ;
;]]; <
client^^ 
.^^ 
HostName^^ 
=^^ 
queueOptions^^ *
.^^* +
Value^^+ 0
.^^0 1
HostName^^1 9
;^^9 :
client`` 
.`` (
ConfigureReceiveMessageAsync`` /
(``/ 0
MessageHandler``0 >
)``> ?
;``? @
}aa 	
privatecc 
staticcc 
asynccc 
Taskcc !
MessageHandlercc" 0
(cc0 1
objectcc1 7
modelcc8 =
,cc= >!
BasicDeliverEventArgscc? T
	eventArgsccU ^
)cc^ _
{dd 	
usingee 
varee 
scopeee 
=ee 
_serviceProvideree .
.ee. /
CreateAsyncScopeee/ ?
(ee? @
)ee@ A
;eeA B
varff 
loggerff 
=ff 
scopeff 
.ff 
ServiceProviderff .
.ff. /
GetRequiredServiceff/ A
<ffA B
ILoggerffB I
<ffI J
ProgramffJ Q
>ffQ R
>ffR S
(ffS T
)ffT U
;ffU V
varhh 
bodyhh 
=hh 
	eventArgshh  
.hh  !
Bodyhh! %
.hh% &
ToArrayhh& -
(hh- .
)hh. /
;hh/ 0
varii 
messageii 
=ii 
Encodingii "
.ii" #
UTF8ii# '
.ii' (
	GetStringii( 1
(ii1 2
bodyii2 6
)ii6 7
;ii7 8
loggerkk 
.kk 
LogInformationkk !
(kk! "
messagekk" )
)kk) *
;kk* +
varmm 

jsonObjectmm 
=mm 

JsonObjectmm '
.mm' (
Parsemm( -
(mm- .
messagemm. 5
)mm5 6
;mm6 7
varnn 
messageTypenn 
=nn 

jsonObjectnn (
[nn( )
$strnn) /
]nn/ 0
.nn0 1
GetValuenn1 9
<nn9 :
stringnn: @
>nn@ A
(nnA B
)nnB C
;nnC D
switchpp 
(pp 
messageTypepp 
)pp 
{qq 
casess 
nameofss 
(ss 
UpdateProductEventss .
)ss. /
:ss/ 0
vartt 
updateProductEventtt *
=tt+ ,
JsonSerializertt- ;
.tt; <
Deserializett< G
<ttG H
UpdateProductEventttH Z
>ttZ [
(tt[ \
messagett\ c
)ttc d
;ttd e
varuu 
eventHandleruu $
=uu% &
scopeuu' ,
.uu, -
ServiceProvideruu- <
.uu< =

GetServiceuu= G
<uuG H
ICartEventHandleruuH Y
>uuY Z
(uuZ [
)uu[ \
;uu\ ]
awaitvv 
eventHandlervv &
.vv& '%
UpdateItemInAllCartsAsyncvv' @
(vv@ A
updateProductEventvvA S
)vvS T
;vvT U
breakww 
;ww 
defaultxx 
:xx 
throwyy 
newyy 
ArgumentExceptionyy /
(yy/ 0
$"yy0 2
$stryy2 N
{yyN O
messageTypeyyO Z
}yyZ [
$stryy[ `
{yy` a
messageyya h
}yyh i
$stryyi k
"yyk l
)yyl m
;yym n
}zz 
;zz 
}{{ 	
}|| 
}}} Ó<
ÉC:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\PL\WebAPI\Middlewares\IdentityLoggingMiddleware.cs
	namespace 	
CartService
 
. 
PL 
. 
WebAPI 
.  
Middlewares  +
{ 
public 

class %
IdentityLoggingMiddleware *
(* +
RequestDelegate+ :
next; ?
)? @
{ 
public 
Task 
Invoke 
( 
HttpContext &
httpContext' 2
,2 3
ILogger4 ;
<; <%
IdentityLoggingMiddleware< U
>U V
loggerW ]
)] ^
{		 	
var

 
user

 
=

 
httpContext

 "
.

" #
User

# '
;

' (
var 
userInfo 
= 
GetIdentityInfo *
(* +
user+ /
)/ 0
;0 1
logger 
. 
LogInformation !
(! "
userInfo" *
)* +
;+ ,
return 
next 
( 
httpContext #
)# $
;$ %
} 	
private 
static 
string 
GetIdentityInfo -
(- .
ClaimsPrincipal. =
user> B
)B C
{ 	
var 
stringBuilder 
= 
new  #
StringBuilder$ 1
(1 2
$num2 5
)5 6
;6 7
AddClaimsInfo 
( 
stringBuilder '
,' (
user) -
.- .
Claims. 4
)4 5
;5 6
AddIdentitiesInfo 
( 
stringBuilder +
,+ ,
user- 1
.1 2

Identities2 <
)< =
;= >
return 
stringBuilder  
.  !
ToString! )
() *
)* +
;+ ,
} 	
private 
static 
void 
AddClaimsInfo )
() *
StringBuilder* 7
stringBuilder8 E
,E F
IEnumerableG R
<R S
ClaimS X
>X Y
claimsZ `
)` a
{ 	
stringBuilder 
. 

AppendLine $
($ %
$str% 0
)0 1
;1 2
foreach 
( 
var 
claim 
in !
claims" (
)( )
{   
stringBuilder!! 
.!! 

AppendLine!! (
(!!( )
$"!!) +
$str!!+ .
"!!. /
)!!/ 0
;!!0 1
stringBuilder"" 
."" 

AppendLine"" (
(""( )
$""") +
$str""+ 9
{""9 :
claim"": ?
.""? @
Subject""@ G
}""G H
$str""H N
"""N O
)""O P
;""P Q
stringBuilder## 
.## 

AppendLine## (
(##( )
$"##) +
$str##+ 8
{##8 9
claim##9 >
.##> ?
Issuer##? E
}##E F
$str##F L
"##L M
)##M N
;##N O
stringBuilder$$ 
.$$ 

AppendLine$$ (
($$( )
$"$$) +
$str$$+ 7
{$$7 8
claim$$8 =
.$$= >
Value$$> C
}$$C D
$str$$D J
"$$J K
)$$K L
;$$L M
stringBuilder%% 
.%% 

AppendLine%% (
(%%( )
$"%%) +
$str%%+ ;
{%%; <
claim%%< A
.%%A B
	ValueType%%B K
}%%K L
$str%%L P
"%%P Q
)%%Q R
;%%R S
stringBuilder&& 
.&& 

AppendLine&& (
(&&( )
$"&&) +
$str&&+ /
"&&/ 0
)&&0 1
;&&1 2
}'' 
stringBuilder(( 
.(( 

AppendLine(( $
((($ %
$str((% (
)((( )
;(() *
})) 	
private++ 
static++ 
void++ 
AddIdentitiesInfo++ -
(++- .
StringBuilder++. ;
stringBuilder++< I
,++I J
IEnumerable++K V
<++V W
ClaimsIdentity++W e
>++e f
identitites++g r
)++r s
{,, 	
stringBuilder-- 
.-- 

AppendLine-- $
(--$ %
$str--% 4
)--4 5
;--5 6
foreach.. 
(.. 
var.. 
identity.. !
in.." $
identitites..% 0
)..0 1
{// 
stringBuilder00 
.00 

AppendLine00 (
(00( )
$"00) +
$str00+ .
"00. /
)00/ 0
;000 1
stringBuilder11 
.11 

AppendLine11 (
(11( )
$"11) +
$str11+ ;
{11; <
identity11< D
.11D E
Actor11E J
?11J K
.11K L
Name11L P
}11P Q
$str11Q W
"11W X
)11X Y
;11Y Z
stringBuilder22 
.22 

AppendLine22 (
(22( )
$"22) +
$str22+ >
{22> ?
identity22? G
.22G H
Name22H L
}22L M
$str22M S
"22S T
)22T U
;22U V
stringBuilder33 
.33 

AppendLine33 (
(33( )
$"33) +
$str33+ D
{33D E
identity33E M
.33M N
AuthenticationType33N `
}33` a
$str33a g
"33g h
)33h i
;33i j
stringBuilder44 
.44 

AppendLine44 (
(44( )
$"44) +
$str44+ A
{44A B
identity44B J
.44J K
IsAuthenticated44K Z
}44Z [
$str44[ _
"44_ `
)44` a
;44a b
stringBuilder55 
.55 

AppendLine55 (
(55( )
$"55) +
$str55+ ?
{55? @
identity55@ H
.55H I
NameClaimType55I V
}55V W
$str55W [
"55[ \
)55\ ]
;55] ^
stringBuilder66 
.66 

AppendLine66 (
(66( )
$"66) +
$str66+ ?
{66? @
identity66@ H
.66H I
RoleClaimType66I V
}66V W
$str66W [
"66[ \
)66\ ]
;66] ^
stringBuilder77 
.77 

AppendLine77 (
(77( )
$"77) +
$str77+ 7
{777 8
identity778 @
.77@ A
Label77A F
}77F G
$str77G K
"77K L
)77L M
;77M N
stringBuilder88 
.88 

AppendLine88 (
(88( )
$"88) +
$str88+ /
"88/ 0
)880 1
;881 2
}99 
stringBuilder:: 
.:: 

AppendLine:: $
(::$ %
$str::% (
)::( )
;::) *
};; 	
}<< 
public?? 

static?? 
class?? +
LogIdentityMiddlewareExtensions?? 7
{@@ 
publicAA 
staticAA 
IApplicationBuilderAA )
UseIdentityLoggingAA* <
(AA< =
thisAA= A
IApplicationBuilderAAB U
builderAAV ]
)AA] ^
{BB 	
returnCC 
builderCC 
.CC 
UseMiddlewareCC (
<CC( )%
IdentityLoggingMiddlewareCC) B
>CCB C
(CCC D
)CCD E
;CCE F
}DD 	
}EE 
}FF ≤$
|C:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\PL\WebAPI\Controllers\V1\CartsController.cs
	namespace 	
CartService
 
. 
PL 
. 
WebAPI 
.  
Controllers  +
.+ ,
V1, .
;. /
[

 
ApiController

 
,

 

ApiVersion

 
(

 
$num

 
)

 
]

  
[ 
Route 
( 
$str (
)( )
]) *
[ 
Produces 	
(	 

$str
 
) 
] 
[ 
Consumes 	
(	 

$str
 
) 
] 
[ 
	Authorize 

(
 
Roles 
= 
$" 
{ 
ApplicationRoles &
.& '
Manager' .
}. /
$str/ 1
{1 2
ApplicationRoles2 B
.B C
StoreCustomerC P
}P Q
"Q R
)R S
]S T
public 
class 
CartsController 
( 
ICartLogicHandler .
	cartLogic/ 8
)8 9
:: ;
ControllerBase< J
{ 
[   
HttpGet   
]   
[!! 
MapToApiVersion!! 
(!! 
$num!! 
)!! 
,!! 
MapToApiVersion!! *
(!!* +
$num!!+ .
)!!. /
]!!/ 0
[""  
ProducesResponseType"" 
("" 
StatusCodes"" %
.""% &
Status200OK""& 1
)""1 2
]""2 3
public## 

async## 
Task## 
<## 
IActionResult## #
>### $
GetAllCarts##% 0
(##0 1
)##1 2
{$$ 
var%% 
carts%% 
=%% 
await%% 
	cartLogic%% #
.%%# $
GetAllCartsAsync%%$ 4
(%%4 5
)%%5 6
;%%6 7
return'' 
Ok'' 
('' 
carts'' 
)'' 
;'' 
}(( 
[77 
HttpGet77 
(77 
$str77 
)77 
]77 
[88  
ProducesResponseType88 
(88 
StatusCodes88 %
.88% &
Status200OK88& 1
)881 2
]882 3
public99 

IActionResult99 
GetCartInfo99 $
(99$ %
string99% +
cartId99, 2
)992 3
{:: 
var;; 
cart;; 
=;; 
	cartLogic;; 
.;; 
GetCartAsync;; )
(;;) *
cartId;;* 0
);;0 1
;;;1 2
return== 
Ok== 
(== 
cart== 
)== 
;== 
}>> 
[UU 
HttpPostUU 
(UU 
$strUU 
)UU 
]UU  
[VV  
ProducesResponseTypeVV 
(VV 
StatusCodesVV %
.VV% &
Status200OKVV& 1
)VV1 2
]VV2 3
publicWW 

asyncWW 
TaskWW 
<WW 
IActionResultWW #
>WW# $
AddItmeToCartWW% 2
(WW2 3
stringWW3 9
cartIdWW: @
,WW@ A
[WWB C
FromBodyWWC K
]WWK L
ProductItemWWM X
productItemWWY d
)WWd e
{XX 
awaitYY 
	cartLogicYY 
.YY 
AddItemToCartAsyncYY *
(YY* +
cartIdYY+ 1
,YY1 2
productItemYY3 >
)YY> ?
;YY? @
returnZZ 
OkZZ 
(ZZ 
)ZZ 
;ZZ 
}[[ 
[kk 

HttpDeletekk 
(kk 
$strkk )
)kk) *
]kk* +
[ll  
ProducesResponseTypell 
(ll 
StatusCodesll %
.ll% &
Status200OKll& 1
)ll1 2
]ll2 3
publicmm 

asyncmm 
Taskmm 
<mm 
IActionResultmm #
>mm# $
AddItmeToCartmm% 2
(mm2 3
stringmm3 9
cartIdmm: @
,mm@ A
intmmB E
itemIdmmF L
)mmL M
{nn 
awaitoo 
	cartLogicoo 
.oo #
RemoveItemFromCartAsyncoo /
(oo/ 0
cartIdoo0 6
,oo6 7
itemIdoo8 >
)oo> ?
;oo? @
returnpp 
Okpp 
(pp 
)pp 
;pp 
}qq 
}rr “
|C:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\PL\WebAPI\Controllers\V2\CartsController.cs
	namespace 	
CartService
 
. 
PL 
. 
WebAPI 
.  
Controllers  +
.+ ,
V2, .
;. /
[		 
ApiController		 
,		 

ApiVersion		 
(		 
$num		 
)		 
]		  
[

 
Route

 
(

 
$str

 (
)

( )
]

) *
[ 
Produces 	
(	 

$str
 
) 
] 
[ 
Consumes 	
(	 

$str
 
) 
] 
[ 
	Authorize 

(
 
Roles 
= 
$" 
{ 
ApplicationRoles &
.& '
Manager' .
}. /
$str/ 1
{1 2
ApplicationRoles2 B
.B C
StoreCustomerC P
}P Q
"Q R
)R S
]S T
public 
class 
CartsController 
( 
ICartLogicHandler .
	cartLogic/ 8
)8 9
:: ;
ControllerBase< J
{ 
[ 
HttpGet 
( 
$str 
) 
, 
MapToApiVersion )
() *
$num* -
)- .
]. /
[  
ProducesResponseType 
( 
StatusCodes %
.% &
Status200OK& 1
)1 2
]2 3
public 

async 
Task 
< 
IActionResult #
># $
GetCartInfo% 0
(0 1
string1 7
cartId8 >
)> ?
{   
var!! 
cart!! 
=!! 
await!! 
	cartLogic!! "
.!!" #
GetCartAsync!!# /
(!!/ 0
cartId!!0 6
)!!6 7
;!!7 8
return## 
Ok## 
(## 
cart## 
.## 
Items## 
)## 
;## 
}$$ 
}%% Ã
uC:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\PL\WebAPI\ConfigureSwaggerOptions.cs
public 
class #
ConfigureSwaggerOptions $
:% &
IConfigureOptions' 8
<8 9
SwaggerGenOptions9 J
>J K
{ 
private		 
readonly		 *
IApiVersionDescriptionProvider		 3
	_provider		4 =
;		= >
public 
#
ConfigureSwaggerOptions "
(" #*
IApiVersionDescriptionProvider# A
providerB J
)J K
=>L N
	_provider 
= 
provider 
; 
public 

void 
	Configure 
( 
SwaggerGenOptions +
options, 3
)3 4
{ 
foreach 
( 
var 
description  
in! #
	_provider$ -
.- ."
ApiVersionDescriptions. D
)D E
{ 	
options 
. 

SwaggerDoc 
( 
description 
. 
	GroupName #
,# $
new 
OpenApiInfo 
(  
)  !
{ 
Title 
= 
$" 
$str *
{* +
description+ 6
.6 7

ApiVersion7 A
}A B
"B C
,C D
Version 
= 
description )
.) *

ApiVersion* 4
.4 5
ToString5 =
(= >
)> ?
,? @
} 
) 
; 
} 	
} 
} ùE
gC:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\DependencyInjection.cs
	namespace 	
CartService
 
{ 
internal 
static 
class 
DependencyInjection -
{ 
internal 
static 
void 
AddServices (
(( )
this) -
IServiceCollection. @
servicesA I
,I J
IConfigurationK Y
configurationZ g
)g h
{ 	
services 
. 
	AddScoped 
< 
IRepository *
<* +
Cart+ /
>/ 0
,0 1
CartRepository2 @
>@ A
(A B
)B C
;C D
services 
. 
	AddScoped 
< 
ICartLogicHandler 0
,0 1
CartLogicHandler2 B
>B C
(C D
)D E
;E F
services 
. 
	AddScoped 
< 
ICartEventHandler 0
,0 1
CartEventHandler2 B
>B C
(C D
)D E
;E F
services 
. 
AddSingleton !
<! "!
IDbConnectionProvider" 7
,7 8$
DbLiteConnectionProvider9 Q
>Q R
(R S
_S T
=>U W
new $
DbLiteConnectionProvider 0
(0 1
)1 2
{3 4
ConnectionString5 E
=F G
configurationH U
.U V
GetConnectionStringV i
(i j
$strj r
)r s
}t u
)u v
;v w
services## 
.## 
AddSingleton## !
<##! "
IQueueClient##" .
,##. /
RabbitQueue##0 ;
>##; <
(##< =
)##= >
;##> ?
services$$ 
.$$ 
	Configure$$ 
<$$ 
CartQueueOptions$$ /
>$$/ 0
($$0 1
configuration$$1 >
.$$> ?

GetSection$$? I
($$I J
$str$$J U
)$$U V
)$$V W
;$$W X
}%% 	
internal'' 
static'' 
void'' 
AddVersioning'' *
(''* +
this''+ /
IServiceCollection''0 B
services''C K
)''K L
{(( 	
services** 
.** 
AddApiVersioning** %
(**% &
opt**& )
=>*** ,
{++ 
opt,, 
.,, 
ReportApiVersions,, %
=,,& '
true,,( ,
;,,, -
opt-- 
.-- /
#AssumeDefaultVersionWhenUnspecified-- 7
=--8 9
true--: >
;--> ?
opt.. 
... 
DefaultApiVersion.. %
=..& '
new..( +

ApiVersion.., 6
(..6 7
$num..7 8
,..8 9
$num..: ;
)..; <
;..< =
opt// 
.// 
ApiVersionReader// $
=//% &
new//' *&
UrlSegmentApiVersionReader//+ E
(//E F
)//F G
;//G H
}00 
)00 
.11 
AddMvc11 
(11 
)11 
.22 
AddApiExplorer22 
(22 
opt22 
=>22  "
{33 
opt55 
.55 
GroupNameFormat55 #
=55$ %
$str55& ,
;55, -
opt66 
.66 %
SubstituteApiVersionInUrl66 -
=66. /
true660 4
;664 5
}77 
)77 
;77 
}88 	
internal:: 
static:: 
void:: 

AddSwagger:: '
(::' (
this::( ,
IServiceCollection::- ?
services::@ H
)::H I
{;; 	
services== 
.== 
AddTransient== !
<==! "
IConfigureOptions==" 3
<==3 4
SwaggerGenOptions==4 E
>==E F
,==F G#
ConfigureSwaggerOptions==H _
>==_ `
(==` a
)==a b
;==b c
services?? 
.?? 
AddSwaggerGen?? "
(??" #
options??# *
=>??+ -
{@@ 
varAA 
xmlFilenameAA 
=AA  !
$"AA" $
{AA$ %
AssemblyAA% -
.AA- . 
GetExecutingAssemblyAA. B
(AAB C
)AAC D
.AAD E
GetNameAAE L
(AAL M
)AAM N
.AAN O
NameAAO S
}AAS T
$strAAT X
"AAX Y
;AAY Z
optionsBB 
.BB 
IncludeXmlCommentsBB *
(BB* +
PathBB+ /
.BB/ 0
CombineBB0 7
(BB7 8

AppContextBB8 B
.BBB C
BaseDirectoryBBC P
,BBP Q
xmlFilenameBBR ]
)BB] ^
)BB^ _
;BB_ `
}CC 
)CC 
;CC 
servicesEE 
.EE #
AddEndpointsApiExplorerEE ,
(EE, -
)EE- .
;EE. /
}FF 	
internalHH 
staticHH 
voidHH #
ConfigureAuthenticationHH 4
(HH4 5
thisHH5 9
IServiceCollectionHH: L
servicesHHM U
)HHU V
{II 	
servicesJJ 
.JJ 
AddAuthenticationJJ &
(JJ& '
optionsJJ' .
=>JJ/ 1
{KK 
optionsLL 
.LL 
DefaultSchemeLL %
=LL& '(
CookieAuthenticationDefaultsLL( D
.LLD E 
AuthenticationSchemeLLE Y
;LLY Z
optionsMM 
.MM "
DefaultChallengeSchemeMM .
=MM/ 0
$strMM1 7
;MM7 8
}NN 
)NN 
.OO 
	AddCookieOO 
(OO (
CookieAuthenticationDefaultsOO 3
.OO3 4 
AuthenticationSchemeOO4 H
)OOH I
.PP 
AddJwtBearerPP 
(PP 
JwtBearerDefaultsPP +
.PP+ , 
AuthenticationSchemePP, @
,PP@ A
optionsPPB I
=>PPJ L
{QQ 
optionsRR 
.RR 
	AuthorityRR !
=RR" #
$strRR$ <
;RR< =
optionsSS 
.SS %
TokenValidationParametersSS 1
.SS1 2
ValidateAudienceSS2 B
=SSC D
falseSSE J
;SSJ K
}TT 
)TT 
.UU 
AddOpenIdConnectUU 
(UU 
$strUU $
,UU$ %
optionsUU& -
=>UU. 0
{VV 
optionsWW 
.WW 
	AuthorityWW !
=WW" #
$strWW$ <
;WW< =
optionsYY 
.YY 
ClientIdYY  
=YY! "
$strYY# 1
;YY1 2
optionsZZ 
.ZZ 
ClientSecretZZ $
=ZZ% &
$strZZ' /
;ZZ/ 0
options[[ 
.[[ 
ResponseType[[ $
=[[% &
$str[[' -
;[[- .
options]] 
.]] 
Scope]] 
.]] 
Clear]] #
(]]# $
)]]$ %
;]]% &
options^^ 
.^^ 
Scope^^ 
.^^ 
Add^^ !
(^^! "
$str^^" *
)^^* +
;^^+ ,
options__ 
.__ 
Scope__ 
.__ 
Add__ !
(__! "
$str__" +
)__+ ,
;__, -
options`` 
.`` )
GetClaimsFromUserInfoEndpoint`` 5
=``6 7
true``8 <
;``< =
optionsbb 
.bb 
MapInboundClaimsbb (
=bb) *
falsebb+ 0
;bb0 1
optionsdd 
.dd 
ClaimActionsdd $
.dd$ %

MapJsonKeydd% /
(dd/ 0
$strdd0 6
,dd6 7
$strdd8 >
,dd> ?
$strdd@ F
)ddF G
;ddG H
optionsee 
.ee %
TokenValidationParametersee 1
.ee1 2
NameClaimTypeee2 ?
=ee@ A
$streeB H
;eeH I
optionsff 
.ff %
TokenValidationParametersff 1
.ff1 2
RoleClaimTypeff2 ?
=ff@ A
$strffB H
;ffH I
optionsll 
.ll 

SaveTokensll "
=ll# $
truell% )
;ll) *
}mm 
)mm 
;mm 
}nn 	
internalpp 
staticpp 
voidpp "
ConfigureAuthorizationpp 3
(pp3 4
thispp4 8
IServiceCollectionpp9 K
servicesppL T
)ppT U
{qq 	
servicesrr 
.rr 
AddAuthorizationrr %
(rr% &
)rr& '
;rr' (
}ss 	
}tt 
}uu ›
wC:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\DAL\Repositories\Common\IRepository.cs
	namespace 	
CartService
 
. 
DAL 
. 
Repositories &
.& '
Common' -
;- .
public 
	interface 
IRepository 
< 
T 
> 
{ 
Task 
< 	
IEnumerable	 
< 
T 
> 
> 
GetAllEntitiesAsync ,
(, -
)- .
;. /
Task 
< 	
T	 

>
 
GetEntityAsync 
( 
string !
id" $
)$ %
;% &
Task 
AddEntityAsync	 
( 
T 
	newEntity #
)# $
;$ %
Task 
UpdateEntityAsync	 
( 
string !
entityId" *
,* +
T, -
updatedEntity. ;
); <
;< =
}		 †
ÅC:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\DAL\Repositories\Common\IDbConnectionProvider.cs
	namespace 	
CartService
 
. 
DAL 
. 
Repositories &
.& '
Common' -
{ 
public 

	interface !
IDbConnectionProvider *
{ 
public 
ILiteDatabaseAsync !
GetConnection" /
(/ 0
)0 1
;1 2
} 
}		 €
ÑC:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\DAL\Repositories\Common\DbLiteConnectionProvider.cs
	namespace 	
CartService
 
. 
DAL 
. 
Repositories &
.& '
Common' -
{ 
public 

class $
DbLiteConnectionProvider )
:* +!
IDbConnectionProvider, A
{ 
public 
required 
string 
ConnectionString /
{0 1
get2 5
;5 6
set7 :
;: ;
}< =
private		 
ILiteDatabaseAsync		 "
_connection		# .
;		. /
public

 
ILiteDatabaseAsync

 !
GetConnection

" /
(

/ 0
)

0 1
{ 	
_connection 
??= 
new 
LiteDatabaseAsync  1
(1 2
ConnectionString2 B
)B C
;C D
return 
_connection 
; 
} 	
} 
} Ç
zC:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\DAL\Repositories\Common\BaseRepository.cs
	namespace 	
CartService
 
. 
DAL 
. 
Repositories &
.& '
Common' -
;- .
public 
abstract 
class 
BaseRepository $
<$ %
T% &
>& '
:( )
IRepository* 5
<5 6
T6 7
>7 8
{		 
private

 
readonly

 !
IDbConnectionProvider

 *
_connectionProvider

+ >
;

> ?
	protected 
BaseRepository 
( !
IDbConnectionProvider 2
connectionProvider3 E
)E F
{ 
_connectionProvider 
= 
connectionProvider 0
;0 1
} 
public 

abstract 
Task 
< 
IEnumerable $
<$ %
T% &
>& '
>' (
GetAllEntitiesAsync) <
(< =
)= >
;> ?
public 

abstract 
Task 
< 
T 
> 
GetEntityAsync *
(* +
string+ 1
id2 4
)4 5
;5 6
public 

abstract 
Task 
AddEntityAsync '
(' (
T( )
	newEntity* 3
)3 4
;4 5
public 

abstract 
Task 
UpdateEntityAsync *
(* +
string+ 1
entityToUpdateId2 B
,B C
TD E
updatedEntityF S
)S T
;T U
	protected 
ILiteDatabaseAsync  
OpenConnection! /
(/ 0
)0 1
=> 

_connectionProvider 
. 
GetConnection ,
(, -
)- .
;. /
} †
}C:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\Common\Security\Identity\ApplicationRoles.cs
	namespace 	
CartService
 
. 
Common 
. 
Security %
.% &
Identity& .
;. /
public 
static 
class 
ApplicationRoles $
{ 
public 

const 
string 
Manager 
=  !
$str" +
;+ ,
public 

const 
string 
StoreCustomer %
=& '
$str( 7
;7 8
} à"
sC:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\DAL\Repositories\CartRepository.cs
	namespace 	
CartService
 
. 
DAL 
. 
Repositories &
{ 
public 

class 
CartRepository 
:  !
BaseRepository" 0
<0 1
Cart1 5
>5 6
{ 
public		 
CartRepository		 
(		 !
IDbConnectionProvider		 3
connectionProvider		4 F
)		F G
:

 
base

 
(

 
connectionProvider

 %
)

% &
{

' (
}

( )
public 
override 
async 
Task "
<" #
IEnumerable# .
<. /
Cart/ 3
>3 4
>4 5
GetAllEntitiesAsync6 I
(I J
)J K
{ 	
var 

connection 
= 
OpenConnection +
(+ ,
), -
;- .
var 
cartsCollection 
=  !

connection" ,
., -
GetCollection- :
<: ;
Cart; ?
>? @
(@ A
)A B
;B C
var 
carts 
= 
await 
cartsCollection -
.- .
FindAllAsync. :
(: ;
); <
;< =
return 
carts 
; 
} 	
public 
override 
async 
Task "
<" #
Cart# '
>' (
GetEntityAsync) 7
(7 8
string8 >
id? A
)A B
{ 	
var 

connection 
= 
OpenConnection +
(+ ,
), -
;- .
var 
carts 
= 

connection "
." #
GetCollection# 0
<0 1
Cart1 5
>5 6
(6 7
)7 8
;8 9
var 
cart 
= 
await 
carts "
." #
FindOneAsync# /
(/ 0
cart0 4
=>5 7
cart8 <
.< =
Id= ?
==@ B
idC E
)E F
;F G
return 
cart 
; 
} 	
public 
override 
async 
Task "
AddEntityAsync# 1
(1 2
Cart2 6
	newEntity7 @
)@ A
{ 	
var   

connection   
=   
OpenConnection   +
(  + ,
)  , -
;  - .
var!! 
carts!! 
=!! 

connection!! "
.!!" #
GetCollection!!# 0
<!!0 1
Cart!!1 5
>!!5 6
(!!6 7
)!!7 8
;!!8 9
await## 
carts## 
.## 
InsertAsync## #
(### $
	newEntity##$ -
)##- .
;##. /
}$$ 	
public&& 
override&& 
async&& 
Task&& "
UpdateEntityAsync&&# 4
(&&4 5
string&&5 ;
entityToUpdateId&&< L
,&&L M
Cart&&N R
updatedEntity&&S `
)&&` a
{'' 	
var(( 

connection(( 
=(( 
OpenConnection(( +
(((+ ,
)((, -
;((- .
var)) 
carts)) 
=)) 

connection)) "
.))" #
GetCollection))# 0
<))0 1
Cart))1 5
>))5 6
())6 7
)))7 8
;))8 9
var++ 
cartToUpdate++ 
=++ 
await++ $
carts++% *
.++* +
FindOneAsync+++ 7
(++7 8
cart++8 <
=>++= ?
cart++@ D
.++D E
Id++E G
==++H J
entityToUpdateId++K [
)++[ \
;++\ ]
await,, 
carts,, 
.,, 
UpdateAsync,, #
(,,# $
cartToUpdate,,$ 0
.,,0 1
Id,,1 3
,,,3 4
updatedEntity,,5 B
),,B C
;,,C D
}-- 	
}.. 
}// é2
pC:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\Common\Messaging\RabbitQueue.cs
	namespace 	
CartService
 
. 
Common 
. 
	Messaging &
;& '
public 
class 
RabbitQueue 
: 
IQueueClient '
,' (
IDisposable) 4
{ 
public		 

string		 
HostName		 
{		 
get		  
;		  !
set		" %
;		% &
}		' (
public

 

string

 
	QueueName

 
{

 
get

 !
;

! "
set

# &
;

& '
}

( )
private 
IConnection 
_connection #
;# $
private 
IChannel 
_channel 
; 
public 

async 
Task 
SendMessageAsync &
(& '
string' -
message. 5
,5 6
CancellationToken7 H
cancellationTokenI Z
=[ \
default] d
)d e
{ 
await !
InitializeClientAsync #
(# $
cancellationToken$ 5
)5 6
;6 7
var 
body 
= 
Encoding 
. 
UTF8  
.  !
GetBytes! )
() *
message* 1
)1 2
;2 3
await 
_channel 
. 
BasicPublishAsync (
(( )

routingKey) 3
:3 4
	QueueName5 >
,> ?
body) -
:- .
body/ 3
,3 4
	mandatory) 2
:2 3
false4 9
,9 :
exchange) 1
:1 2
string3 9
.9 :
Empty: ?
,? @
cancellationToken) :
:: ;
cancellationToken< M
)M N
;N O
} 
public 

async 
Task 
< 
string 
> 
ReceiveMessageAsync 1
(1 2
CancellationToken2 C
cancellationTokenD U
=V W
defaultX _
)_ `
{ 
await !
InitializeClientAsync #
(# $
cancellationToken$ 5
)5 6
;6 7
var   
result   
=   
await   
_channel   #
.  # $
BasicGetAsync  $ 1
(  1 2
	QueueName  2 ;
,  ; <
false  = B
,  B C
cancellationToken  D U
)  U V
;  V W
var!! 
message!! 
=!! 
Encoding!! 
.!! 
UTF8!! #
.!!# $
	GetString!!$ -
(!!- .
result!!. 4
.!!4 5
Body!!5 9
.!!9 :
Span!!: >
)!!> ?
;!!? @
return"" 
message"" 
;"" 
}## 
public%% 

async%% 
Task%% (
ConfigureReceiveMessageAsync%% 2
(%%2 3
AsyncEventHandler%%3 D
<%%D E!
BasicDeliverEventArgs%%E Z
>%%Z [
eventMessageHandler%%\ o
,%%o p
CancellationToken	%%q Ç
cancellationToken
%%É î
=
%%ï ñ
default
%%ó û
)
%%û ü
{&& 
await'' !
InitializeClientAsync'' #
(''# $
cancellationToken''$ 5
)''5 6
;''6 7
var)) 
consumer)) 
=)) 
new)) &
AsyncEventingBasicConsumer)) 5
())5 6
_channel))6 >
)))> ?
;))? @
consumer** 
.** 
ReceivedAsync** 
+=** !
eventMessageHandler**" 5
;**5 6
await,, 
_channel,, 
.,, 
BasicConsumeAsync,, (
(,,( )
queue,,) .
:,,. /
	QueueName,,0 9
,,,9 :
autoAck--) 0
:--0 1
true--2 6
,--6 7
consumer..) 1
:..1 2
consumer..3 ;
,..; <
cancellationToken//) :
://: ;
cancellationToken//< M
)//M N
;//N O
}00 
private44 
async44 
Task44 !
InitializeClientAsync44 ,
(44, -
CancellationToken44- >
cancellationToken44? P
)44P Q
{55 
if77 

(77 
_connection77 
is77 
not77 
null77 #
&&77$ &
_channel77' /
is770 2
not773 6
null777 ;
)77; <
{88 	
return99 
;99 
}:: 	
var== 
factory== 
=== 
new== 
ConnectionFactory== +
{==, -
HostName==. 6
===7 8
HostName==9 A
}==B C
;==C D
_connection?? 
=?? 
await?? 
factory?? #
.??# $!
CreateConnectionAsync??$ 9
(??9 :
cancellationToken??: K
)??K L
;??L M
_channel@@ 
=@@ 
await@@ 
_connection@@ $
.@@$ %
CreateChannelAsync@@% 7
(@@7 8
cancellationToken@@8 I
:@@I J
cancellationToken@@K \
)@@\ ]
;@@] ^
_BB 	
=BB
 
awaitBB 
_channelBB 
.BB 
QueueDeclareAsyncBB ,
(BB, -
queueBB- 2
:BB2 3
	QueueNameBB4 =
,BB= >
durableCC, 3
:CC3 4
trueCC5 9
,CC9 :
	exclusiveDD, 5
:DD5 6
falseDD7 <
,DD< =

autoDeleteEE, 6
:EE6 7
falseEE8 =
,EE= >
cancellationTokenFF, =
:FF= >
cancellationTokenFF? P
)FFP Q
;FFQ R
}GG 
publicII 

voidII 
DisposeII 
(II 
)II 
{JJ 
_channelKK 
?KK 
.KK 
DisposeKK 
(KK 
)KK 
;KK 
_connectionLL 
?LL 
.LL 
DisposeLL 
(LL 
)LL 
;LL 
}MM 
}NN ∂
qC:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\Common\Messaging\IQueueClient.cs
	namespace 	
CartService
 
. 
Common 
. 
	Messaging &
;& '
public 
	interface 
IQueueClient 
{ 
string 

HostName 
{ 
get 
; 
set 
; 
}  !
string 

	QueueName 
{ 
get 
; 
set 
;  
}! "
Task

 
SendMessageAsync

	 
(

 
string

  
message

! (
,

( )
CancellationToken

* ;
cancellationToken

< M
=

N O
default

P W
)

W X
;

X Y
Task 
< 	
string	 
> 
ReceiveMessageAsync $
($ %
CancellationToken% 6
cancellationToken7 H
=I J
defaultK R
)R S
;S T
Task (
ConfigureReceiveMessageAsync	 %
(% &
AsyncEventHandler& 7
<7 8!
BasicDeliverEventArgs8 M
>M N
eventMessageHandlerO b
,b c
CancellationTokend u
cancellationToken	v á
=
à â
default
ä ë
)
ë í
;
í ì
} í
C:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\Common\Exceptions\NotFoundCartItemException.cs
	namespace 	
CartService
 
. 
Common 
. 

Exceptions '
;' (
public 
class %
NotFoundCartItemException &
(& '
string' -
message. 5
)5 6
:7 8
	Exception9 B
(B C
messageC J
)J K
{L M
}N Oï
kC:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\Common\Messaging\IEvent.cs
	namespace 	
CartService
 
. 
Common 
. 
	Messaging &
;& '
public 
	interface 
IEvent 
{ 
string 

Type 
{ 
get 
; 
} 
string 

Version 
{ 
get 
; 
} 
} ≈
uC:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\Common\Messaging\CartQueueOptions.cs
	namespace 	
CatalogService
 
. 
Infrastructure '
.' (
Services( 0
.0 1
CartService1 <
;< =
public 
class 
CartQueueOptions 
{ 
public 

string 
	QueueName 
{ 
get !
;! "
set# &
;& '
}( )
public 

string 
HostName 
{ 
get  
;  !
set" %
;% &
}' (
} í
C:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\Common\Exceptions\ValidationFailedException.cs
	namespace 	
CartService
 
. 
Common 
. 

Exceptions '
;' (
public 
class %
ValidationFailedException &
(& '
string' -
message. 5
)5 6
:7 8
	Exception9 B
(B C
messageC J
)J K
{L M
}M Nö
oC:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\Common\Entities\ProductItem.cs
	namespace 	
CartService
 
. 
Common 
. 
Entities %
;% &
public 
class 
ProductItem 
{ 
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
public 

string 
Name 
{ 
get 
; 
set !
;! "
}# $
public 

string 
Image 
{ 
get 
; 
set "
;" #
}$ %
public 

decimal 
Price 
{ 
get 
; 
set  #
;# $
}% &
public		 

int		 
Quantity		 
{		 
get		 
;		 
set		 "
;		" #
}		$ %
public 

ProductItem 
( 
int 
id 
, 
string %
name& *
,* +
decimal, 3
price4 9
,9 :
int; >
quantity? G
,G H
stringI O
imageP U
=V W
$strX d
)d e
{ 
Id 

= 
id 
; 
Name 
= 
name 
; 
Price 
= 
price 
; 
Quantity 
= 
quantity 
; 
Image 
= 
image 
; 
} 
} Ç
hC:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\Common\Entities\Cart.cs
	namespace 	
CartService
 
. 
Common 
. 
Entities %
;% &
public 
class 
Cart 
{ 
public 

string 
Id 
{ 
get 
; 
set 
;  
}! "
public

 

List

 
<

 
ProductItem

 
>

 
Items

 "
{

# $
get

% (
;

( )
init

* .
;

. /
}

0 1
=

2 3
[

4 5
]

5 6
;

6 7
public 

void 
AddItem 
( 
ProductItem #
item$ (
)( )
{ 
Items 
. 
Add 
( 
item 
) 
; 
} 
public 

void 

RemoveItem 
( 
ProductItem &
item' +
)+ ,
{ 
Items 
. 
Remove 
( 
item 
) 
; 
} 
public 

void 

RemoveItem 
( 
int 
itemId %
)% &
{ 
var 
itemForRemoval 
= 
Items "
." #
FirstOrDefault# 1
(1 2
item2 6
=>7 9
item: >
.> ?
Id? A
==B D
itemIdE K
)K L
;L M
if 

( 
itemForRemoval 
is 
null "
)" #
{ 	
throw 
new %
NotFoundCartItemException /
(/ 0
$"0 2
$str2 ;
{; <
itemId< B
}B C
$strC f
{f g
Idg i
}i j
$strj l
"l m
)m n
;n o
} 	
Items 
. 
Remove 
( 
itemForRemoval #
)# $
;$ %
}   
}!! Ë
sC:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\BLL\CartLogic\ICartLogicHandler.cs
	namespace 	
CartService
 
. 
BLL 
. 
	CartLogic #
{ 
public 

	interface 
ICartLogicHandler &
{ 
Task 
< 
IEnumerable 
< 
Cart 
> 
> 
GetAllCartsAsync  0
(0 1
)1 2
;2 3
Task 
< 
Cart 
> 
GetCartAsync 
(  
string  &
cartId' -
)- .
;. /
Task		 
AddItemToCartAsync		 
(		  
string		  &
cartId		' -
,		- .
ProductItem		/ :
productItem		; F
)		F G
;		G H
Task

 #
RemoveItemFromCartAsync

 $
(

$ %
string

% +
cartId

, 2
,

2 3
int

4 7
itemId

8 >
)

> ?
;

? @
} 
} ã
sC:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\BLL\CartLogic\ICartEventHandler.cs
	namespace 	
CartService
 
. 
BLL 
. 
	CartLogic #
{ 
public 

	interface 
ICartEventHandler &
{ 
Task %
UpdateItemInAllCartsAsync &
(& '
UpdateProductEvent' 9
productEvent: F
)F G
;G H
} 
}		 é
{C:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\BLL\CartLogic\Events\UpdateProductEvent.cs
	namespace 	
CartService
 
. 
BLL 
. 
	CartLogic #
.# $
Events$ *
{ 
public 

class 
UpdateProductEvent #
:$ %
IEvent& ,
{ 
public 
string 
Type 
=> 
nameof $
($ %
UpdateProductEvent% 7
)7 8
;8 9
public 
string 
Version 
=>  
$str! (
;( )
public

 
int

 
?

 
	ProductId

 
{

 
get

  #
;

# $
private

% ,
set

- 0
;

0 1
}

2 3
public 
string 
? 
NewName 
{  
get! $
;$ %
private& -
set. 1
;1 2
}3 4
public 
string 
? 
NewDescription %
{& '
get( +
;+ ,
private- 4
set5 8
;8 9
}: ;
public 
string 
? 
NewImage 
{  !
get" %
;% &
private' .
set/ 2
;2 3
}4 5
public 
int 
? 
NewCategoryId !
{" #
get$ '
;' (
private) 0
set1 4
;4 5
}6 7
public 
decimal 
? 
NewPrice  
{! "
get# &
;& '
private( /
set0 3
;3 4
}5 6
public 
int 
? 
	NewAmount 
{ 
get  #
;# $
private% ,
set- 0
;0 1
}2 3
public 
UpdateProductEvent !
(! "
int 
? 
	productId 
, 
string 
? 
newName 
, 
string 
? 
newDescription "
," #
string 
? 
newImage 
, 
int 
? 
newCategoryId 
, 
decimal 
? 
newPrice 
, 
int 
? 
	newAmount 
) 
{ 	
	ProductId 
= 
	productId !
;! "
NewName 
= 
newName 
; 
NewDescription 
= 
newDescription +
;+ ,
NewImage 
= 
newImage 
;  
NewCategoryId 
= 
newCategoryId )
;) *
NewPrice   
=   
newPrice   
;    
	NewAmount!! 
=!! 
	newAmount!! !
;!!! "
}"" 	
}## 
}$$ Æ.
rC:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\BLL\CartLogic\CartLogicHandler.cs
	namespace 	
CartService
 
. 
BLL 
. 
	CartLogic #
{ 
public 

class 
CartLogicHandler !
:" #
ICartLogicHandler$ 5
{ 
private		 
readonly		 
IRepository		 $
<		$ %
Cart		% )
>		) *
_cartRepository		+ :
;		: ;
public 
CartLogicHandler 
(  
IRepository  +
<+ ,
Cart, 0
>0 1
cartRepository2 @
)@ A
{ 	
_cartRepository 
= 
cartRepository ,
;, -
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
Cart& *
>* +
>+ ,
GetAllCartsAsync- =
(= >
)> ?
{ 	
var 
carts 
= 
await 
_cartRepository -
.- .
GetAllEntitiesAsync. A
(A B
)B C
;C D
return 
carts 
; 
} 	
public 
async 
Task 
< 
Cart 
> 
GetCartAsync  ,
(, -
string- 3
cartId4 :
): ;
{ 	
var 
cart 
= 
await 
_cartRepository ,
., -
GetEntityAsync- ;
(; <
cartId< B
)B C
;C D
if 
( 
cart 
== 
null 
) 
{ 
throw 
new  
KeyNotFoundException .
(. /
$"/ 1
$str1 ?
{? @
cartId@ F
}F G
$strG W
"W X
)X Y
;Y Z
} 
return   
cart   
;   
}!! 	
public## 
async## 
Task## 
AddItemToCartAsync## ,
(##, -
string##- 3
cartId##4 :
,##: ;
ProductItem##< G
productItem##H S
)##S T
{$$ 	
var%% 
validationResult%%  
=%%! "
new%%# &
CartItemValidator%%' 8
(%%8 9
)%%9 :
.%%: ;
Validate%%; C
(%%C D
productItem%%D O
)%%O P
;%%P Q
if&& 
(&& 
!&& 
validationResult&& !
.&&! "
IsValid&&" )
)&&) *
{'' 
var(( 
errorMessages(( !
=((" #
validationResult(($ 4
.((4 5
Errors((5 ;
.((; <
Select((< B
(((B C
failure((C J
=>((K M
failure((N U
.((U V
ErrorMessage((V b
)((b c
;((c d
var)) 
errorMessagesString)) '
=))( )
string))* 0
.))0 1
Join))1 5
())5 6
Environment))6 A
.))A B
NewLine))B I
,))I J
errorMessages))K X
)))X Y
;))Y Z
throw** 
new** %
ValidationFailedException** 3
(**3 4
errorMessagesString**4 G
)**G H
;**H I
}++ 
var-- 
cart-- 
=-- 
await--  
GetOrCreateCartAsync-- 1
(--1 2
cartId--2 8
)--8 9
;--9 :
cart.. 
... 
AddItem.. 
(.. 
productItem.. $
)..$ %
;..% &
await00 
_cartRepository00 !
.00! "
UpdateEntityAsync00" 3
(003 4
cart004 8
.008 9
Id009 ;
,00; <
cart00= A
)00A B
;00B C
}11 	
public33 
async33 
Task33 #
RemoveItemFromCartAsync33 1
(331 2
string332 8
cartId339 ?
,33? @
int33A D
itemId33E K
)33K L
{44 	
var55 
cart55 
=55 
await55 
_cartRepository55 ,
.55, -
GetEntityAsync55- ;
(55; <
cartId55< B
)55B C
;55C D
cart66 
.66 

RemoveItem66 
(66 
itemId66 "
)66" #
;66# $
await88 
_cartRepository88 !
.88! "
UpdateEntityAsync88" 3
(883 4
cart884 8
.888 9
Id889 ;
,88; <
cart88= A
)88A B
;88B C
}99 	
private;; 
async;; 
Task;; 
<;; 
Cart;; 
>;;   
GetOrCreateCartAsync;;! 5
(;;5 6
string;;6 <
cartId;;= C
);;C D
{<< 	
var== 
	foundCart== 
=== 
await== !
_cartRepository==" 1
.==1 2
GetEntityAsync==2 @
(==@ A
cartId==A G
)==G H
;==H I
if>> 
(>> 
	foundCart>> 
!=>> 
null>> !
)>>! "
{?? 
return@@ 
	foundCart@@  
;@@  !
}AA 
varCC 
newCartCC 
=CC 
newCC 
CartCC "
{DD 
IdEE 
=EE 
cartIdEE 
}FF 
;FF 
awaitGG 
_cartRepositoryGG !
.GG! "
AddEntityAsyncGG" 0
(GG0 1
newCartGG1 8
)GG8 9
;GG9 :
returnII 
newCartII 
;II 
}JJ 	
}KK 
}LL –
sC:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\BLL\CartLogic\CartItemValidator.cs
	namespace 	
CartService
 
. 
BLL 
. 
	CartLogic #
{ 
public 

class 
CartItemValidator "
:# $
AbstractValidator% 6
<6 7
ProductItem7 B
>B C
{ 
public 
CartItemValidator  
(  !
)! "
{		 	
RuleFor

 
(

 
item

 
=>

 
item

  
.

  !
Id

! #
)

# $
. 
NotEmpty 
( 
) 
. 
WithMessage '
(' (
$str( 9
)9 :
;: ;
RuleFor 
( 
item 
=> 
item  
.  !
Name! %
)% &
. 
NotEmpty 
( 
) 
. 
WithMessage '
(' (
$str( ;
); <
;< =
RuleFor 
( 
item 
=> 
item  
.  !
Price! &
)& '
. 
NotEmpty 
( 
) 
. 
WithMessage '
(' (
$str( <
)< =
;= >
} 	
} 
} ≈
rC:\Users\Aleksandr_Goriachkin\Desktop\NetProgramAdvanced\CartService\CartService\BLL\CartLogic\CartEventHandler.cs
	namespace 	
CartService
 
. 
BLL 
. 
	CartLogic #
{ 
public 

class 
CartEventHandler !
:" #
ICartEventHandler$ 5
{ 
private		 
readonly		 
IRepository		 $
<		$ %
Cart		% )
>		) *
_cartRepository		+ :
;		: ;
public 
CartEventHandler 
(  
IRepository  +
<+ ,
Cart, 0
>0 1
cartRepository2 @
)@ A
{ 	
_cartRepository 
= 
cartRepository ,
;, -
} 	
public 
async 
Task %
UpdateItemInAllCartsAsync 3
(3 4
UpdateProductEvent4 F
productEventG S
)S T
{ 	
var 
updatedProductId  
=! "
productEvent# /
./ 0
	ProductId0 9
;9 :
var 
carts 
= 
await 
_cartRepository -
.- .
GetAllEntitiesAsync. A
(A B
)B C
;C D
foreach 
( 
var 
cart 
in  
carts! &
)& '
{ 
foreach 
( 
var 
item !
in" $
cart% )
.) *
Items* /
./ 0
Where0 5
(5 6
item6 :
=>; =
item> B
.B C
IdC E
==F H
updatedProductIdI Y
)Y Z
)Z [
{ 
item 
. 
Quantity !
=" #
productEvent$ 0
.0 1
	NewAmount1 :
??; =
item> B
.B C
QuantityC K
;K L
item 
. 
Price 
=  
productEvent! -
.- .
NewPrice. 6
??7 9
item: >
.> ?
Price? D
;D E
item 
. 
Name 
= 
productEvent  ,
., -
NewName- 4
??5 7
item8 <
.< =
Name= A
;A B
item 
. 
Image 
=  
productEvent! -
.- .
NewImage. 6
??7 9
item: >
.> ?
Image? D
;D E
} 
await 
_cartRepository %
.% &
UpdateEntityAsync& 7
(7 8
cart8 <
.< =
Id= ?
,? @
cartA E
)E F
;F G
}   
}!! 	
}"" 
}## 