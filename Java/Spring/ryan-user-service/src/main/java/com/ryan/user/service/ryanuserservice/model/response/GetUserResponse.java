package com.ryan.user.service.ryanuserservice.model.response;

import com.ryan.user.service.ryanuserservice.datastore.document.User;

public class GetUserResponse extends BaseResponse {
    private User user;

    public GetUserResponse(User user) {
        if (user != null) {

        }
        this.user = user;
    }
}
