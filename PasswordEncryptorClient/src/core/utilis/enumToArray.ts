export function enumConversion(enumType: { [s: string]: unknown; } | ArrayLike<unknown>){

const enumerableObject=[];

const keys=Object.keys(enumType);

const values=Object.values(enumType);

for(let i=0;i<Math.ceil(keys.length/2);i++){
    enumerableObject.push({key:keys[i],value:values[i]})
}
return enumerableObject;

}