import { URLSearchParams } from 'angular2/http';

export const requestParams = ()=>
{
    var params = new URLSearchParams();
    params.set("__RequestVerificationToken", $("input[type='hidden'][name='__RequestVerificationToken']").val());
    return params;
};