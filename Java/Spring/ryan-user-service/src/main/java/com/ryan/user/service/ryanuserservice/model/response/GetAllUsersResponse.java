package com.ryan.user.service.ryanuserservice.model.response;

import com.ryan.user.service.ryanuserservice.datastore.document.User;

import java.util.List;

public class GetAllUsersResponse extends BaseResponse {
    List<User> users;

    public GetAllUsersResponse(List<User> users) {
        this.success = true;
        this.message = "";
        this.users = users;
    }

    public GetAllUsersResponse(List<User> users, String message) {
        this.success = true;
        this.message = message;
        this.users = users;
    }

    public List<User> getUsers() {
        return users;
    }
}
