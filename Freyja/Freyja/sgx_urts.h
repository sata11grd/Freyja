/*
 *   Copyright(C) 2011-2018 Intel Corporation All Rights Reserved.
 *
 *   The source code, information  and  material ("Material") contained herein is
 *   owned  by Intel Corporation or its suppliers or licensors, and title to such
 *   Material remains  with Intel Corporation  or its suppliers or licensors. The
 *   Material  contains proprietary information  of  Intel or  its  suppliers and
 *   licensors. The  Material is protected by worldwide copyright laws and treaty
 *   provisions. No  part  of  the  Material  may  be  used,  copied, reproduced,
 *   modified, published, uploaded, posted, transmitted, distributed or disclosed
 *   in any way  without Intel's  prior  express written  permission. No  license
 *   under  any patent, copyright  or  other intellectual property rights  in the
 *   Material  is  granted  to  or  conferred  upon  you,  either  expressly,  by
 *   implication, inducement,  estoppel or  otherwise.  Any  license  under  such
 *   intellectual  property  rights must  be express  and  approved  by  Intel in
 *   writing.
 *
 *   *Third Party trademarks are the property of their respective owners.
 *
 *   Unless otherwise  agreed  by Intel  in writing, you may not remove  or alter
 *   this  notice or  any other notice embedded  in Materials by Intel or Intel's
 *   suppliers or licensors in any way.
 *
 */

#ifndef _SGX_URTS_H_
#define _SGX_URTS_H_

#include "sgx_attributes.h"
#include "sgx_error.h"
#include "sgx_eid.h"
#include "sgx_defs.h"
#include "sgx_key.h"
#include "sgx_report.h"
#if defined(_MSC_VER)
#include <windows.h>
#endif

#define MAX_EX_FEATURES_COUNT 32

#define SGX_CREATE_ENCLAVE_EX_PCL_BIT_IDX           0U
#define SGX_CREATE_ENCLAVE_EX_PCL                  (1U << SGX_CREATE_ENCLAVE_EX_PCL_BIT_IDX) // Bit 0 for the Protected Code Loader

#define SGX_CREATE_ENCLAVE_EX_SWITCHLESS_BIT_IDX    1U
#define SGX_CREATE_ENCLAVE_EX_SWITCHLESS           (1U << SGX_CREATE_ENCLAVE_EX_SWITCHLESS_BIT_IDX) // Bit 1 for Switchless Runtime System

#define SGX_CREATE_ENCLAVE_EX_KSS_BIT_IDX           2U
#define SGX_CREATE_ENCLAVE_EX_KSS                  (1U << SGX_CREATE_ENCLAVE_EX_KSS_BIT_IDX)  // Bit 2 for Key Separation & Sharing 

#pragma pack(push, 1)

 /* Structure for KSS feature */
typedef struct _sgx_kss_config_t
{
	sgx_config_id_t config_id;
	sgx_config_svn_t config_svn;
} sgx_kss_config_t;

#pragma pack(pop)

#ifdef __cplusplus
extern "C" {
#endif

	typedef uint8_t sgx_launch_token_t[1024];

	/* Convenient macro to be passed to sgx_create_enclave(). */
#if !defined(NDEBUG) || defined(EDEBUG)
#define SGX_DEBUG_FLAG 1
#else
#define SGX_DEBUG_FLAG 0
#endif


#if defined(_MSC_VER)
#ifdef UNICODE
#define sgx_create_enclave  sgx_create_enclavew
#define sgx_create_enclave_ex       sgx_create_enclave_exw
#else
#define sgx_create_enclave  sgx_create_enclavea
#define sgx_create_enclave_ex       sgx_create_enclave_exa
#endif /* !UNICODE */

/*
@attr: On success, 'misc_attr->secs_attr' is filled with secs attributes. On error, 'misc_attr->secs_attr' is filled with platform capability.
*/
	sgx_status_t SGXAPI sgx_create_enclavew(const LPCWSTR file_name, const int debug, sgx_launch_token_t *launch_token, int *launch_token_updated, sgx_enclave_id_t *enclave_id, sgx_misc_attribute_t *misc_attr);
	sgx_status_t SGXAPI sgx_create_enclavea(const LPCSTR file_name, const int debug, sgx_launch_token_t *launch_token, int *launch_token_updated, sgx_enclave_id_t *enclave_id, sgx_misc_attribute_t *misc_attr);

	sgx_status_t SGXAPI sgx_create_enclave_exw(const LPCWSTR file_name, const int debug, sgx_launch_token_t *launch_token, int *launch_token_updated, sgx_enclave_id_t *enclave_id, sgx_misc_attribute_t *misc_attr, const uint32_t ex_features, const void* ex_features_p[32]);
	sgx_status_t SGXAPI sgx_create_enclave_exa(const LPCSTR file_name, const int debug, sgx_launch_token_t *launch_token, int *launch_token_updated, sgx_enclave_id_t *enclave_id, sgx_misc_attribute_t *misc_attr, const uint32_t ex_features, const void* ex_features_p[32]);

	sgx_status_t SGXAPI sgx_get_target_info(const sgx_enclave_id_t enclave_id, sgx_target_info_t* target_info);


#elif defined(__GNUC__)
	sgx_status_t SGXAPI sgx_create_enclave(const char *file_name, const int debug, sgx_launch_token_t *launch_token, int *launch_token_updated, sgx_enclave_id_t *enclave_id, sgx_misc_attribute_t *misc_attr);
#endif

	sgx_status_t SGXAPI sgx_destroy_enclave(const sgx_enclave_id_t enclave_id);

#ifdef __cplusplus
}
#endif


#endif
