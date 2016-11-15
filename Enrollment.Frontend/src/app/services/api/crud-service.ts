import {Response, URLSearchParams} from "@angular/http";
import {TypedHttpService} from "../http/typed-http";


export abstract class CRUDService<T> {
  constructor(
    protected http: TypedHttpService,
    private apiEndpointUrl: string
  ) {
  }

  list(params: URLSearchParams): Promise<T[]> {
    const url = `${this.apiEndpointUrl}?${params.toString()}`;
    return this.http.get<GenericDataResult<T> | ErrorResult>(url)
      .then(result => {
        if (result.succeeded) {
          const guidResult = result as GenericDataResult<T>;
          return guidResult.data;
        } else {
          return this.ProcessFailedRequest(result);
        }
      })
      .catch((response: Response) => {
        console.log(response);
        return Promise.reject(undefined)
      });
  }

  private ProcessFailedRequest(result: any): Promise<any> {
    const errorResult = result as ErrorResult;
    console.log(errorResult);
    return Promise.reject(errorResult.baseMessage);
  }

  create(model: T): Promise<string> {
    return this.http.post<GenericDataResult<string> | ErrorResult>(this.apiEndpointUrl, model)
      .then((result => {
        if (result.succeeded) {
          const guidResult = result as GenericDataResult<string>;
          return guidResult.data;
        } else {
          return this.ProcessFailedRequest(result);
        }
      }))
      .catch((response: Response) => {
        console.log(response);
        return Promise.reject(undefined)
      });
  }

  read(guid: string, params: URLSearchParams = new URLSearchParams()): Promise<T> {
    const url = `${this.apiEndpointUrl}/${guid}?${params.toString()}`;
    return this.http.get<GenericDataResult<T> | ErrorResult>(url)
      .then(result => {
        if (result.succeeded) {
          const dataResult = result as GenericDataResult<T>;
          return dataResult.data;
        } else {
          return this.ProcessFailedRequest(result);
        }
      })
      .catch((response: Response) => {
        console.log(response);
        return Promise.reject(undefined)
      })
  }

  update(guid: string, model: T): Promise<never> {
    return this.http.update<SuccessApiResult | ErrorResult>(this.apiEndpointUrl + "/" + guid, model)
      .then(result => {
        if (result.succeeded) {
          return Promise.resolve();
        } else {
          return this.ProcessFailedRequest(result);
        }
      })
      .catch((response: Response) => {
        console.log(response);
        return Promise.reject(undefined)
      });
  }

  delete(guid: string): Promise<never> {
    return this.http.delete<SuccessApiResult | ErrorResult>(this.apiEndpointUrl + "/" + guid)
      .then(result => {
        if (result.succeeded) {
          return Promise.resolve();
        } else {
          return this.ProcessFailedRequest(result);
        }
      })
      .catch((response: Response) => {
        console.log(response);
        return Promise.reject(undefined)
      });
  }
}
